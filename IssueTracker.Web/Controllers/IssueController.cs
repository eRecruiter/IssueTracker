using ePunkt.Utilities;
using IssueTracker.Web.Code;
using IssueTracker.Web.Models;
using IssueTracker.Web.ViewModels.Issue;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Settings = IssueTracker.Web.Models.Settings;

namespace IssueTracker.Web.Controllers
{
    [Authorize]
    public class IssueController : Controller
    {
        private readonly Db _db = new Db();

        #region Index
        public ActionResult Index(int? page)
        {
            var viewModel = new IndexViewModel(_db, this.GetCurrentUser(_db), ViewData);

            page = page.HasValue ? page.Value - 1 : 0;
            var issues = from x in _db.Issues
                         where !x.ParentIssueId.HasValue
                         select x;

            var userOptions = new UserOptions(Request.Cookies, Response.Cookies);
            issues = issues.Filter(userOptions).Sort(userOptions);

            viewModel.Issues = issues.Skip(page.Value * Settings.IssuesPerPage).Take(Settings.IssuesPerPage).Include(y => y.Comments).ToList().Select(x => new IndexIssuePartialViewModel(this.GetCurrentUser(_db), x)).ToList();
            viewModel.Total = issues.Count();
            viewModel.Start = page.Value * Settings.IssuesPerPage;
            viewModel.End = viewModel.Start + viewModel.Issues.Count();
            viewModel.Page = page.Value + 1;

            // ReSharper disable once PossibleLossOfFraction
            viewModel.MaxPage = (int)Math.Ceiling((double)(viewModel.Total / Settings.IssuesPerPage)) + 1;

            return View("Index", viewModel);
        }


        public ActionResult Options(string order, string date, string user, string status, string text)
        {
            var userOptions = new UserOptions(Request.Cookies, Response.Cookies);

            // make sure we only update the settings if we get a new value from the request

            if (order.HasValue())
                userOptions.Sorting = order;

            if (user.HasValue())
                userOptions.UserFilter = user;

            if (status.HasValue())
                userOptions.StatusFilter = status;

            if (text.HasValue())
                userOptions.TextFilter = text;

            if (date.HasValue())
                userOptions.DateFilter = date.GetIntOrNull();

            return RedirectToAction("Index");
        }

        #endregion

        #region Details
        public ActionResult Details(int id)
        {
            var issue = _db.Issues.SingleOrDefault(x => x.Id == id);
            if (issue == null)
                return RedirectToAction("Index", "Issue");
            return View(new DetailsIssuePartialViewModel(_db, this.GetCurrentUser(_db), issue, ViewData));
        }
        #endregion

        #region Attachment
        public ActionResult Attachment(int id)
        {
            var comment = _db.Comments.SingleOrDefault(x => x.Id == id);
            if (comment == null)
                return null;
            return File(Path.Combine(Server.MapPath(Settings.AttachmentsPath), comment.AttachmentFileName), Util.GetContentType(comment.AttachmentFileName), comment.AttachmentNiceName);
        }
        #endregion

        #region Add Comment
        [Authorize]
        public ActionResult AddComment(int id)
        {
            var issue = _db.Issues.SingleOrDefault(x => x.Id == id);
            if (issue == null)
                return RedirectToAction("Index", "Issue");
            ViewData.Model = issue;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddComment(int id, string comment, string email, string status)
        {
            var issue = _db.Issues.SingleOrDefault(x => x.Id == id);
            if (issue == null)
                return RedirectToAction("Index", "Issue");
            status = status ?? issue.Status; //to make sure it's not null.
            comment = comment ?? ""; //to make sure it's not null.

            if (!string.IsNullOrEmpty(comment) || !string.IsNullOrEmpty(email) || !status.Equals(issue.Status, StringComparison.InvariantCultureIgnoreCase))
            {
                var newComment = new Comment { DateOfCreation = DateTime.Now, Creator = User.Identity.Name, IssueId = issue.Id, Text = comment };

                if (!string.IsNullOrEmpty(email))
                    newComment.Email = email;
                if (!status.Equals(issue.Status, StringComparison.InvariantCultureIgnoreCase) && _db.Status.Any(x => x.Name == status))
                {
                    newComment.OldStatus = issue.Status;
                    newComment.NewStatus = status;
                    issue.Status = status;
                }

                _db.Comments.Add(newComment);
            }

            if (!string.IsNullOrEmpty(email))
            {
                var requestUrl = Request.Url;

                var currentUser = this.GetCurrentUser(_db);
                var body = currentUser.Name + " wants to let you know about changes on issue #" + issue.Id +
                           (string.IsNullOrEmpty(comment) ? ". " : ": <br /><br /><i>" + Server.HtmlEncode(comment).Replace("\r", "").Replace("\n", "<br />") + "</i><br /><br />") + "Please visit " +
                           (requestUrl == null ? "" : requestUrl.ToString().Replace("AddComment", "Details")) + " for all the details.</div><br /><br />Have a nice day! Yours,<br />~ the IssueTracker E-Mail-Monkey";

                var fromEmail = currentUser.Email;
                if (string.IsNullOrEmpty(fromEmail))
                    fromEmail = "somebody@somewhere.com";

                Util.SendMail(currentUser.Name, fromEmail, email, email, "Info from " + currentUser.Name + " about issue #" + issue.Id, body);
            }

            _db.SaveChanges();

            return RedirectToAction("Details", "Issue", new
            {
                id
            });
        }
        #endregion

        #region Update / Delete
        [Authorize]
        [HttpPost]
        public ActionResult Update(FormCollection form, int? id, string status, string assignedTo, string text)
        {
            if (id.HasValue)
            {
                if (form["Delete"] != null)
                    return Delete(id.Value);
                Update(id.Value, status, assignedTo, text);

                return RedirectToAction("Details", "Issue", new { id });
            }

            foreach (var issueId in GetSelectedIssues(form))
                if (form["Delete"] != null)
                    Delete(_db, issueId);
                else
                    Update(issueId, status, assignedTo, text);

            return RedirectToAction("Index", "Issue");
        }

        private void Update(int id, string status, string assignedTo, string text)
        {
            var issue = _db.Issues.SingleOrDefault(x => x.Id == id);
            if (issue != null)
            {
                var user = Utils.GetAllUsers(_db, ViewData).FirstOrDefault(x => x.Name.Is(assignedTo));
                var hasChanged = false;

                var comment = new Comment { IssueId = issue.Id, DateOfCreation = DateTime.Now, Creator = this.GetCurrentUser(_db).Username };

                if (status.HasValue() && Utils.GetAllStati(_db, ViewData).Any(x => x.Name.Is(status)))
                {
                    if (!issue.Status.Is(status))
                    {
                        comment.OldStatus = issue.Status;
                        comment.NewStatus = status;
                        issue.Status = status;
                        hasChanged = true;
                    }
                }

                if (assignedTo.HasValue())
                {
                    if (assignedTo != null && user != null)
                    {
                        if (!issue.AssignedTo.Is(user.Username))
                        {
                            comment.OldAssignedTo = issue.AssignedTo;
                            comment.NewAssignedTo = user.Username;
                            issue.AssignedTo = user.Username;
                            hasChanged = true;
                        }
                    }
                    else
                    {
                        if (issue.AssignedTo != null)
                        {
                            comment.OldAssignedTo = issue.AssignedTo;
                            comment.NewAssignedTo = null;
                            issue.AssignedTo = null;
                            hasChanged = true;
                        }
                    }
                }

                if (text.HasValue())
                    hasChanged = true;

                if (hasChanged)
                {
                    comment.Text = text ?? "";
                    _db.Comments.Add(comment);
                }

                _db.SaveChanges();
            }
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Delete(_db, id);
            return RedirectToAction("Index", "Issue");
        }

        private void Delete(Db db, int issueId)
        {
            var issue = db.Issues.SingleOrDefault(x => x.Id == issueId);

            if (issue != null)
            {
                foreach (var childIssueId in db.Issues.Where(x => x.ParentIssueId == issue.Id).Select(x => x.Id).ToList())
                    Delete(db, childIssueId);

                _db.Issues.Remove(issue);
                _db.SaveChanges();
            }
        }

        private IEnumerable<int> GetSelectedIssues(FormCollection form)
        {
            return from x in _db.Issues.Select(x => x.Id).ToList()
                   where form["issue" + x].HasValue() && !form["issue" + x].Is("false")
                   select x;
        }
        #endregion
    }
}

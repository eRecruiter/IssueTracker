using System;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using IssueTracker.Models;
using IssueTracker.ViewModels.Issue;
using System.Collections.Generic;

namespace IssueTracker.Controllers {
    public class IssueController : Controller {

        #region Index
        public ActionResult Index(int? page) {
            var viewModel = new IndexViewModel(ViewData);

            page = page.HasValue ? page.Value - 1 : 0;

            var context = new SiteDataContext();
            var issues = from x in context.IssueViews
                         where !x.ParentIssueId.HasValue
                         select x;

            if (this.GetCurrentUser() == null)
                issues = issues.Where(x => x.IsPublic);

            viewModel.StatusFilter = Request.Cookies["status"] != null ? Request.Cookies["status"].Value : "";
            if (viewModel.StatusFilter.HasValue())
                issues = issues.Where(x => x.Status.ToLower() == viewModel.StatusFilter.ToLower());

            viewModel.TimeFilter =Request.Cookies["time"] != null ? Convert.ToInt32(Request.Cookies["time"].Value) : 0;
            if (viewModel.TimeFilter > 0)
                issues = issues.Where(x => x.DateOfUpdate >= DateTime.Now.AddDays(-viewModel.TimeFilter));

            viewModel.TextFilter = Request.Cookies["search"] != null ? Request.Cookies["search"].Value : "";
            if (viewModel.TextFilter.HasValue())
                issues = issues.Where(x => SqlMethods.Like(x.Text, "%" + viewModel.TextFilter + "%") || SqlMethods.Like(x.StackTrace, "%" + viewModel.TextFilter + "%") || SqlMethods.Like(x.ServerVariables, "%" + viewModel.TextFilter + "%"));

            viewModel.AssignedToFilter = Request.Cookies["assignedTo"] != null ? Request.Cookies["assignedTo"].Value : "";
            if (viewModel.AssignedToFilter.HasValue() && viewModel.AssignedToFilter.Is("-"))
                issues = issues.Where(x => x.AssignedTo == null);
            else if (viewModel.AssignedToFilter.HasValue())
                issues = issues.Where(x => x.AssignedTo.ToLower() == viewModel.AssignedToFilter.ToLower());

            viewModel.Order = Request.Cookies["order"] != null ? Request.Cookies["order"].Value : "date";

            var duplicateId = (int?)ViewData["duplicateId"];
            if (duplicateId.HasValue)
                issues = issues.Where(x => x.Id != duplicateId.Value);

            var sortedIssues = issues.OrderByDescending(x => x.DateOfUpdate);
            if (viewModel.Order.Is("status"))
                sortedIssues = issues.OrderBy(x => x.Status);
            else if (viewModel.Order.Is("comments"))
                sortedIssues = issues.OrderByDescending(x => x.NumberOfComments);

            viewModel.Issues = sortedIssues.Skip(page.Value * Settings.IssuesPerPage).Take(Settings.IssuesPerPage).Select(x => new IndexIssuePartialViewModel(x, ViewData)).ToList();

            viewModel.Total = sortedIssues.Count();
            viewModel.Start = page.Value * Settings.IssuesPerPage;
            viewModel.End = viewModel.Start + viewModel.Issues.Count();
            viewModel.Page = page.Value + 1;
            viewModel.MaxPage = (int)Math.Ceiling((double)viewModel.Total / (double)Settings.IssuesPerPage) + 1;

            return View(viewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(string order, int? timeFilter, string assignedToFilter, string statusFilter, string textFilter) {
            Response.Cookies.Remove("time");
            Response.Cookies.Remove("status");
            Response.Cookies.Remove("order");

            Response.Cookies["time"].Value = (timeFilter ?? 0).ToString();
            Response.Cookies["time"].Expires = DateTime.MaxValue;

            Response.Cookies["status"].Value = statusFilter ?? "";
            Response.Cookies["status"].Expires = DateTime.MaxValue;

            Response.Cookies["order"].Value = order ?? "";
            Response.Cookies["order"].Expires = DateTime.MaxValue;

            Response.Cookies["assignedTo"].Value = assignedToFilter ?? "";
            Response.Cookies["assignedTo"].Expires = DateTime.MaxValue;

            Response.Cookies["search"].Value = textFilter ?? "";
            Response.Cookies["search"].Expires = DateTime.MaxValue;

            return RedirectToAction("Index", "Issue");
        }
        #endregion

        #region Details
        public ActionResult Details(int id) {
            var context = new SiteDataContext();
            var issue = context.IssueViews.SingleOrDefault(x => x.Id == id);
            if (issue == null || (!issue.IsPublic && this.GetCurrentUser() == null))
                return RedirectToAction("Index", "Issue");
            return View(new DetailsIssuePartialViewModel(issue, ViewData));
        }
        #endregion

        #region Attachment
        public ActionResult Attachment(int id) {
            var context = new SiteDataContext();
            var comment = context.Comments.SingleOrDefault(x => x.Id == id);
            if (comment == null || (!comment.Issue.IsPublic && !User.Identity.IsAuthenticated))
                return null;
            return File(Path.Combine(Server.MapPath(Settings.AttachmentsPath), comment.AttachmentFileName), Util.GetContentType(comment.AttachmentFileName), comment.AttachmentNiceName);
        }
        #endregion

        #region Add Comment
        [Authorize]
        public ActionResult AddComment(int id) {
            var context = new SiteDataContext();
            var issue = context.Issues.SingleOrDefault(x => x.Id == id);
            if (issue == null)
                return RedirectToAction("Index", "Issue");
            ViewData.Model = issue;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddComment(int id, string comment, string email, bool @public, string status) {
            var context = new SiteDataContext();
            var issue = context.Issues.SingleOrDefault(x => x.Id == id);
            if (issue == null)
                return RedirectToAction("Index", "Issue");
            status = status ?? issue.Status; //to make sure it's not null.
            comment = comment ?? ""; //to make sure it's not null.

            if (!string.IsNullOrEmpty(comment) || !string.IsNullOrEmpty(email) || !status.Equals(issue.Status, StringComparison.InvariantCultureIgnoreCase)) {
                var newComment = new Comment();
                newComment.DateOfCreation = DateTime.Now;
                newComment.Creator = User.Identity.Name;
                newComment.IssueId = issue.Id;

                newComment.Text = comment;
                if (!string.IsNullOrEmpty(email))
                    newComment.Email = email;
                if (!status.Equals(issue.Status, StringComparison.InvariantCultureIgnoreCase) && context.Status.Any(x => x.Name == status)) {
                    newComment.OldStatus = issue.Status;
                    newComment.NewStatus = status;
                    issue.Status = status;
                }

                context.Comments.InsertOnSubmit(newComment);
            }

            if (!string.IsNullOrEmpty(email)) {
                var currentUser = this.GetCurrentUser();
                var body = currentUser.Name + " wants to let you know about changes on issue #" + issue.Id + (string.IsNullOrEmpty(comment) ? ". " : ": <br /><br /><i>" + Server.HtmlEncode(comment).Replace("\r", "").Replace("\n", "<br />") + "</i><br /><br />") + "Please visit " + Request.Url.ToString().Replace("AddComment", "Details") + " for all the details.</div><br /><br />Have a nice day! Yours,<br />~ the IssueTracker E-Mail-Monkey";
                Util.SendMail(currentUser.Name, currentUser.Email, email, email, "Info from " + currentUser.Name + " about issue #" + issue.Id, body);
            }

            issue.IsPublic = @public;
            context.SubmitChanges();

            return RedirectToAction("Details", "Issue", new {
                id = id
            });
        }
        #endregion

        #region Update / Delete
        [Authorize]
        [HttpPost]
        public ActionResult Update(FormCollection form, int? id, string status, string assignedTo, string text) {
            if (id.HasValue) {
                if (form["Delete"] != null)
                    return Delete(id.Value);
                else
                    Update(id.Value, status, assignedTo, text);

                return RedirectToAction("Details", "Issue", new { id = id });
            }
            else {
                foreach (var issueId in GetSelectedIssues(form))
                    if (form["Delete"] != null)
                        Delete(issueId);
                    else
                        Update(issueId, status, assignedTo, text);
                return RedirectToAction("Index", "Issue");
            }
        }

        private void Update(int id, string status, string assignedTo, string text) {
            using (var context = new SiteDataContext()) {
                var issue = context.Issues.SingleOrDefault(x => x.Id == id);
                if (issue != null) {
                    var user = Utils.GetAllUsers(ViewData).FirstOrDefault(x => x.Name.Is(assignedTo));
                    var hasChanged = false;

                    var comment = new Comment();
                    comment.IssueId = issue.Id;
                    comment.DateOfCreation = DateTime.Now;
                    comment.Creator = this.GetCurrentUser().Username;

                    if (status.HasValue() && Utils.GetAllStati(ViewData).Any(x => x.Name.Is(status))) {
                        if (!issue.Status.Is(status)) {
                            comment.OldStatus = issue.Status;
                            comment.NewStatus = status;
                            issue.Status = status;
                            hasChanged = true;
                        }
                    }

                    if (assignedTo.HasValue()) {
                        if (assignedTo != null && user != null) {
                            if (!issue.AssignedTo.Is(user.Username)) {
                                comment.OldAssignedTo = issue.AssignedTo;
                                comment.NewAssignedTo = user.Username;
                                issue.AssignedTo = user.Username;
                                hasChanged = true;
                            }
                        }
                        else {
                            if (issue.AssignedTo != null) {
                                comment.OldAssignedTo = issue.AssignedTo;
                                comment.NewAssignedTo = null;
                                issue.AssignedTo = null;
                                hasChanged = true;
                            }
                        }
                    }

                    if (text.HasValue())
                        hasChanged = true;

                    if (hasChanged) {
                        comment.Text = text ?? "";
                        context.Comments.InsertOnSubmit(comment);
                    }

                    context.SubmitChanges();
                }
            }
        }

        [Authorize]
        public ActionResult Delete(int id) {
            using (var context = new SiteDataContext()) {
                var issue = context.Issues.SingleOrDefault(x => x.Id == id);
                if (issue != null) {
                    foreach (var i in context.Issues.Where(x => x.ParentIssueId == issue.Id))
                        Delete(i.Id);
                    context.Comments.DeleteAllOnSubmit(context.Comments.Where(x => x.IssueId == issue.Id));
                    context.SubmitChanges();
                    context.Issues.DeleteAllOnSubmit(context.Issues.Where(x => x.Id == issue.Id));
                    context.SubmitChanges();
                }
            }
            return RedirectToAction("Index", "Issue");
        }

        private IEnumerable<int> GetSelectedIssues(FormCollection form) {
            using (var context = new SiteDataContext()) {
                return from x in context.Issues.Select(x => x.Id).ToList()
                       where form["issue" + x].HasValue() && !form["issue" + x].Is("false")
                       select x;
            }
        }
        #endregion

    }
}

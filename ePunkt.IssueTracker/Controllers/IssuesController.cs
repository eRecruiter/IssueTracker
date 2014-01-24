using ePunkt.IssueTracker.Code;
using ePunkt.IssueTracker.Models;
using ePunkt.IssueTracker.ViewModels.Issues;
using ePunkt.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ePunkt.IssueTracker.Controllers
{
    [Authorize]
    public class IssuesController : Controller
    {
        private readonly Db _db = new Db();

        public ActionResult Index(int? page, string tags)
        {
            var viewModel = new IndexViewModel(_db, ViewData);

            page = page.HasValue ? page.Value - 1 : 0;
            var issues = from x in _db.Issues
                         where !x.ParentIssueId.HasValue
                         select x;
            viewModel.TotalCount = issues.Count();

            SaveUserOptions(null, null, null, null, null, tags);
            var userOptions = new UserOptions(Request.Cookies, Response.Cookies);
            issues = issues.Filter(userOptions).Sort(userOptions);

            viewModel.Issues =
                issues.Skip(page.Value * IssueTrackerSettings.IssuesPerPage)
                    .Take(IssueTrackerSettings.IssuesPerPage)
                    .Include(y => y.Comments).Include(y => y.Tags)
                    .ToList()
                    .Select(x => new IndexIssuePartialViewModel(this.GetCurrentUser(_db), x))
                    .ToList();
            viewModel.FilteredCount = issues.Count();
            viewModel.Start = page.Value * IssueTrackerSettings.IssuesPerPage;
            viewModel.End = viewModel.Start + viewModel.Issues.Count();
            viewModel.Page = page.Value + 1;

            // ReSharper disable once PossibleLossOfFraction
            viewModel.MaxPage = (int)Math.Ceiling((double)(viewModel.FilteredCount / IssueTrackerSettings.IssuesPerPage)) + 1;

            return View("Index", viewModel);
        }


        public ActionResult Options(string order, string date, string user, string status, string text, string tags)
        {
            SaveUserOptions(order, date, user, status, text, tags);
            return RedirectToAction("Index");
        }

        private void SaveUserOptions(string order, string date, string user, string status, string text, string tags)
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

            if (tags.HasValue())
                userOptions.TagsFilter = tags.Split(',').Select(x => x.Trim(' ', ',')).Where(x => x.HasValue());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(FormCollection form, string status, string user)
        {
            foreach (var issueId in GetSelectedIssues(form))
                if (form["Delete"] != null)
                {
                    var deleteService = new DeleteIssueService(_db);
                    deleteService.Delete(issueId);
                }
                else
                {
                    var updateService = new UpdateIssueService(_db, ViewData);
                    updateService.Update(User, issueId, user, status, null);
                }

            return RedirectToAction("Index", "Issues");
        }

        private IEnumerable<int> GetSelectedIssues(FormCollection form)
        {
            return from x in _db.Issues.Select(x => x.Id).ToList()
                   where form["issue" + x].HasValue() && !form["issue" + x].Is("false")
                   select x;
        }
    }
}

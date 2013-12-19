using ePunkt.Utilities;
using IssueTracker.Web.Code;
using IssueTracker.Web.Models;
using IssueTracker.Web.ViewModels.Issues;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Settings = IssueTracker.Web.Models.Settings;

namespace IssueTracker.Web.Controllers
{
    [Authorize]
    public class IssuesController : Controller
    {
        private readonly Db _db = new Db();

        public ActionResult Index(int? page)
        {
            var viewModel = new IndexViewModel(_db, ViewData);

            page = page.HasValue ? page.Value - 1 : 0;
            var issues = from x in _db.Issues
                         where !x.ParentIssueId.HasValue
                         select x;

            var userOptions = new UserOptions(Request.Cookies, Response.Cookies);
            issues = issues.Filter(userOptions).Sort(userOptions);

            viewModel.Issues =
                issues.Skip(page.Value * Settings.IssuesPerPage)
                    .Take(Settings.IssuesPerPage)
                    .Include(y => y.Comments)
                    .ToList()
                    .Select(x => new IndexIssuePartialViewModel(this.GetCurrentUser(_db), x))
                    .ToList();
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

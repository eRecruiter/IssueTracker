using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker.ViewModels.Issue {
    public class DetailsIssuePartialViewModel : IssueTracker.ViewModels.IssuePartialViewModel {

        public DetailsIssuePartialViewModel(IssueView issue, ViewDataDictionary viewData)
            : base(issue, viewData) {

            DateOfCreation = issue.DateOfCreation;
            ParentIssueId = issue.ParentIssueId;
            StackTrace = issue.StackTrace;
            ServerVariables = issue.ServerVariables;
            using (var context = new SiteDataContext()) {
                Comments = (from x in context.Comments
                            where x.IssueId == issue.Id
                            orderby x.DateOfCreation
                            select new DetailsCommentPartialViewModel(x, viewData)).ToList();
            }

            var stati = new List<SelectListItem>();
            stati.AddRange(
                    from x in Utils.GetAllStati(viewData)
                    select new SelectListItem {
                        Text = x.Name,
                        Value = x.Name,
                        Selected = Status.Is(x.Name)
                    }
                );
            AvailableStati = stati;

            var users = new List<SelectListItem>();
            users.Add(new SelectListItem {
                Text = "< none >",
                Value = "",
                Selected = AssignedTo.IsNoE()
            });
            users.AddRange(
                    from x in Utils.GetAllUsers(viewData)
                    select new SelectListItem {
                        Text = x.Name,
                        Value = x.Name,
                        Selected = AssignedTo.Is(x.Name)
                    }
                );
            AvailableUsers = users;
        }


        public DateTime DateOfCreation { get; set; }
        public int? ParentIssueId { get; set; }
        public IEnumerable<DetailsCommentPartialViewModel> Comments { get; set; }
        public string StackTrace { get; set; }
        public string ServerVariables { get; set; }

        public IEnumerable<SelectListItem> AvailableStati { get; private set; }
        public IEnumerable<SelectListItem> AvailableUsers { get; private set; }

    }
}

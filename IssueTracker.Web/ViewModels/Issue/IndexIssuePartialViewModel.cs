using System;
using IssueTracker.Web.Models;

namespace IssueTracker.Web.ViewModels.Issue {
    public class IndexIssuePartialViewModel : IssuePartialViewModel {

        public IndexIssuePartialViewModel(User currentUser, Models.Issue issue)
            : base(issue) {

            CurrentUser = currentUser;

            Comments = issue.NumberOfComments;

            Time = "just now";
            if (issue.DateOfUpdate < DateTime.Now.AddMonths(-1))
                Time = issue.DateOfUpdate.ToString("dd.MM.yyyy");
            else if (issue.DateOfUpdate < DateTime.Now.AddDays(-1))
                Time = (int)Math.Ceiling(((DateTime.Now - issue.DateOfUpdate)).TotalDays) + " days ago";
            else if (issue.DateOfUpdate < DateTime.Now.AddHours(-1))
                Time = (int)Math.Ceiling(((DateTime.Now - issue.DateOfUpdate)).TotalHours) + " hours ago";
            else if (issue.DateOfUpdate < DateTime.Now.AddMinutes(-10))
                Time = (int)Math.Ceiling((DateTime.Now - issue.DateOfUpdate).TotalMinutes) + " min. ago";
        }


        public string Time { get; set; }
        public int Comments { get; set; }

        public User CurrentUser { get; set; }

    }
}

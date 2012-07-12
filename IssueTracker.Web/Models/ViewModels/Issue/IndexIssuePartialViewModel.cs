using System;
using IssueTracker.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace IssueTracker.ViewModels.Issue {
    public class IndexIssuePartialViewModel : IssueTracker.ViewModels.IssuePartialViewModel {

        public IndexIssuePartialViewModel(IssueView issue, ViewDataDictionary viewData)
            : base(issue, viewData) {

            Comments = issue.NumberOfComments ?? 0;

            Time = "just now";
            if (issue.DateOfUpdate < DateTime.Now.AddMonths(-1))
                Time = issue.DateOfUpdate.Value.ToString("dd.MM.yyyy");
            else if (issue.DateOfUpdate < DateTime.Now.AddDays(-1))
                Time = (int)Math.Ceiling(((TimeSpan)(DateTime.Now - issue.DateOfUpdate.Value)).TotalDays) + " days ago";
            else if (issue.DateOfUpdate < DateTime.Now.AddHours(-1))
                Time = (int)Math.Ceiling(((TimeSpan)(DateTime.Now - issue.DateOfUpdate.Value)).TotalHours) + " hours ago";
            else if (issue.DateOfUpdate < DateTime.Now.AddMinutes(-10))
                Time = (int)Math.Ceiling(((TimeSpan)(DateTime.Now - issue.DateOfUpdate.Value)).TotalMinutes) + " min. ago";
        }


        public string Time { get; set; }
        public int Comments { get; set; }

    }
}

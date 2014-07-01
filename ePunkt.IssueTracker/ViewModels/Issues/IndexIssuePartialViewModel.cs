using System;
using ePunkt.IssueTracker.Code;
using ePunkt.IssueTracker.Models;

namespace ePunkt.IssueTracker.ViewModels.Issues {
    public class IndexIssuePartialViewModel : IssuePartialViewModel {

        public IndexIssuePartialViewModel(User currentUser, Models.Issue issue)
            : base(issue) {

            CurrentUser = currentUser;

            Comments = issue.NumberOfComments;

            if (issue.DateOfUpdate.Date == DateTime.Now.ToUniversalTime().Date)
                Time = issue.DateOfUpdate.ToCentralEuropeanTime().ToString("HH:mm");
            else
                Time = issue.DateOfUpdate.ToCentralEuropeanTime().ToString("d.M.");
        }


        public string Time { get; set; }
        public int Comments { get; set; }

        public User CurrentUser { get; set; }
    }
}

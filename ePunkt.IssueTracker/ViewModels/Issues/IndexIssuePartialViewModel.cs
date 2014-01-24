using System;
using ePunkt.IssueTracker.Models;

namespace ePunkt.IssueTracker.ViewModels.Issues {
    public class IndexIssuePartialViewModel : IssuePartialViewModel {

        public IndexIssuePartialViewModel(User currentUser, Models.Issue issue)
            : base(issue) {

            CurrentUser = currentUser;

            Comments = issue.NumberOfComments;

            if (issue.DateOfUpdate.Date == DateTime.Now.Date)
                Time = issue.DateOfUpdate.ToString("HH:mm");
            else
                Time = issue.DateOfUpdate.ToString("d.M.");
        }


        public string Time { get; set; }
        public int Comments { get; set; }

        public User CurrentUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker.ViewModels {
    public abstract class IssuePartialViewModel {


        public IssuePartialViewModel(IssueTracker.Models.Issue issue, ViewDataDictionary viewData) {
            Id = issue.Id;
            Creator = issue.Creator;

            Status = issue.Status;
            AssignedTo = issue.AssignedTo;
            Text = issue.Text;
        }

        public int Id { get; set; }
        public string Creator { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
        public string Text { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models {
    public partial class Issue {
        public int Id { get; set; }
        public string Creator { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
        public string StackTrace { get; set; }
        public string ServerVariables { get; set; }
        public int? ParentIssueId { get; set; }
        public bool IsPublic { get; set; }
        public string AssignedTo { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public Issue ParentIssue { get; set; }
        public ICollection<Issue> ChildIssues { get; set; }

        public int NumberOfComments {
            get {
                return Comments.Count;
            }
        }

        public DateTime DateOfUpdate {
            get {
                if (NumberOfComments <= 0)
                    return DateOfCreation;
                return Comments.Max(x => x.DateOfCreation);
            }
        }

            
    }
}
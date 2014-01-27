using System.Collections.Generic;
using System.Linq;

namespace ePunkt.IssueTracker.ViewModels
{
    public abstract class IssuePartialViewModel
    {
        protected IssuePartialViewModel(Models.Issue issue)
        {
            Id = issue.Id;
            Creator = issue.Creator;

            Status = issue.Status;
            AssignedTo = issue.AssignedTo;
            Text = issue.Text;
            Tags = issue.Tags.Select(x => x.Tag).OrderBy(x => x).ToList();
        }

        public int Id { get; set; }
        public string Creator { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
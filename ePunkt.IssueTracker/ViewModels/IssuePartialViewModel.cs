using System;
using System.Collections.Generic;

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

            var tags = new List<string>();
            foreach (var tag in issue.Tags)
            {
                var tagName = tag.Tag;
                if (tagName.Contains(":"))
                    tagName = tagName.Substring(tagName.IndexOf(":", StringComparison.InvariantCultureIgnoreCase) + 1);
                tags.Add(tagName.Trim());
            }
            Tags = tags;
        }

        public int Id { get; set; }
        public string Creator { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
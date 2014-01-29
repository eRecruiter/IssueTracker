using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ePunkt.IssueTracker.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
        public string StackTrace { get; set; }
        public string ServerVariables { get; set; }
        public int? ParentIssueId { get; set; }
        public string AssignedTo { get; set; }
        public string Version { get; set; }
        public string RemoteHost { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public Issue ParentIssue { get; set; }
        public ICollection<Issue> ChildIssues { get; set; }
        public ICollection<IssueTag> Tags { get; set; }

        public int NumberOfComments
        {
            get
            {
                return Comments == null ? 0 : Comments.Count;
            }
        }

        public DateTime DateOfUpdate
        {
            get
            {
                if (NumberOfComments <= 0)
                    return DateOfCreation;
                return Comments.Max(x => x.DateOfCreation);
            }
        }

        public void AddComment(string creator, string text)
        {
            if (string.IsNullOrEmpty(text))
                return; //we don't accept empty comments

            using (var context = new Db())
            {
                var comment = new Comment { Creator = creator, DateOfCreation = DateTime.Now, Text = text, IssueId = Id };

                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }

        public void AddAttachment(string creator, string niceName, string base64, HttpServerUtility server)
        {
            var extension = Path.GetExtension(niceName);
            var path = Path.Combine(server.MapPath(IssueTrackerSettings.AttachmentsPath), Guid.NewGuid().ToString() + extension);

            var bytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(path, bytes);

            using (var context = new Db())
            {
                var comment = new Comment { Creator = creator, DateOfCreation = DateTime.Now, IssueId = Id, Text = "", AttachmentFileName = Path.GetFileName(path), AttachmentNiceName = niceName };
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }

    }
}
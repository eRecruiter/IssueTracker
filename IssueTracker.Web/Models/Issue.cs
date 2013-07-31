using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace IssueTracker.Models
{
    public partial class Issue
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

        public ICollection<Comment> Comments { get; set; }
        public Issue ParentIssue { get; set; }
        public ICollection<Issue> ChildIssues { get; set; }

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


        public static Issue Create(string creator, string text, string stackTrace, string serverVariables)
        {
            using (var context = new Db())
            {

                //log the clients IP and Host
                //this is especially useful when we don't have any serverVariables and therefore can't identify the caller
                const string serverVariablesIpKey = "REMOTE_ADDR";
                if (HttpContext.Current != null && HttpContext.Current.Request.ServerVariables[serverVariablesIpKey] != null)
                    serverVariables = "IssueTracker Client Remote Address: " + HttpContext.Current.Request.ServerVariables[serverVariablesIpKey] + Environment.NewLine + (serverVariables ?? "");
                const string serverVariablesHostKey = "REMOTE_HOST";
                if (HttpContext.Current != null && HttpContext.Current.Request.ServerVariables[serverVariablesHostKey] != null)
                    serverVariables = "IssueTracker Client Remote Host: " + HttpContext.Current.Request.ServerVariables[serverVariablesHostKey] + Environment.NewLine + (serverVariables ?? "");

                foreach (var discardRule in context.DiscardRules)
                {
                    var discard =
                        !(string.IsNullOrEmpty(discardRule.Creator) && string.IsNullOrEmpty(discardRule.Text) && string.IsNullOrEmpty(discardRule.StackTrace) &&
                          string.IsNullOrEmpty(discardRule.ServerVariables));

                    discard = discard && (string.IsNullOrEmpty(discardRule.Creator) || Regex.IsMatch(creator, discardRule.Creator, RegexOptions.IgnoreCase));
                    discard = discard && (string.IsNullOrEmpty(discardRule.Text) || Regex.IsMatch(text, discardRule.Text, RegexOptions.IgnoreCase));
                    discard = discard && (string.IsNullOrEmpty(discardRule.StackTrace) || Regex.IsMatch(stackTrace, discardRule.StackTrace, RegexOptions.IgnoreCase));
                    discard = discard && (string.IsNullOrEmpty(discardRule.ServerVariables) || Regex.IsMatch(serverVariables, discardRule.ServerVariables, RegexOptions.IgnoreCase));

                    if (discard)
                        return null;
                }


                //get the nice text from the stacktrace for generic asp.net errors
                var uglyTexts = new[] {
                    "Exception of type 'System.Web.HttpUnhandledException' was thrown.",
                    "Error executing child request for handler 'System.Web.Mvc.HttpHandlerUtil+ServerExecuteHttpHandlerWrapper'."
                };
                if (string.IsNullOrWhiteSpace(text) || uglyTexts.Any(x => x.Equals(text, StringComparison.InvariantCultureIgnoreCase)))
                {
                    if (!string.IsNullOrWhiteSpace(stackTrace))
                    {
                        text = stackTrace.Split('\n')[0].Trim();

                        if (text.Contains("---> "))
                            text = text.Substring(text.LastIndexOf("---> ") + 5);
                    }
                }


                var parentIssue = context.Issues.FirstOrDefault(x => x.Text == text && x.StackTrace == stackTrace); //find an identical issue
                var newIssue = new Issue
                {
                    DateOfCreation = DateTime.Now,
                    Creator = Util.MaxLength(creator, 4000),
                    Text = Util.MaxLength(text, 4000),
                    StackTrace = Util.MaxLength(stackTrace, 8000),
                    ServerVariables = Util.MaxLength(serverVariables, 8000),
                    Status = Util.MaxLength(Settings.StatusForNewIssues, 4000),
                    AssignedTo = null
                };
                context.Issues.Add(newIssue);
                context.SaveChanges();

                if (parentIssue != null)
                {
                    newIssue.ParentIssueId = parentIssue.Id;

                    var comment = new Comment
                    {
                        Creator = Util.MaxLength(creator, 4000),
                        DateOfCreation = DateTime.Now,
                        DuplicateIssueId = newIssue.Id,
                        IssueId = parentIssue.Id,
                        Text = ""
                    };
                    context.Comments.Add(comment);

                    var status = context.Status.FirstOrDefault(x => x.Name.ToLower() == parentIssue.Status);
                    if (status != null && status.Reactivate)
                        parentIssue.Status = Util.MaxLength(Settings.StatusForNewIssues, 4000);
                    else
                        newIssue.Status = parentIssue.Status;
                }

                context.SaveChanges();
                return newIssue;
            }
        }


        public void AddComment(string creator, string text)
        {
            if (string.IsNullOrEmpty(text))
                return; //we don't accept empty comments

            using (var context = new Db())
            {
                var comment = new Comment();
                comment.Creator = creator;
                comment.DateOfCreation = DateTime.Now;
                comment.Text = text;
                comment.IssueId = Id;

                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }

        public void AddAttachment(string creator, string niceName, string base64, HttpServerUtility server)
        {
            var extension = Path.GetExtension(niceName);
            var path = Path.Combine(server.MapPath(Settings.AttachmentsPath), Guid.NewGuid().ToString() + extension);

            var bytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(path, bytes);

            using (var context = new Db())
            {
                var comment = new Comment();
                comment.Creator = creator;
                comment.DateOfCreation = DateTime.Now;
                comment.IssueId = Id;
                comment.Text = "";
                comment.AttachmentFileName = Path.GetFileName(path);
                comment.AttachmentNiceName = niceName;
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }

    }
}
using ePunkt.IssueTracker.Models;
using JetBrains.Annotations;
using System;
using System.Linq;
using System.Web;

namespace ePunkt.IssueTracker.Code
{
    public class CreateIssueService
    {
        private readonly Db _db;

        public CreateIssueService([NotNull]Db db)
        {
            _db = db;
        }

        [CanBeNull]
        public Issue Create([CanBeNull]string creator, [CanBeNull]string text, [CanBeNull]string stackTrace, [CanBeNull] string serverVariables)
        {
            //log the clients IP and Host
            //this is especially useful when we don't have any serverVariables and therefore can't identify the caller
            const string serverVariablesIpKey = "REMOTE_ADDR";
            if (HttpContext.Current != null && HttpContext.Current.Request.ServerVariables[serverVariablesIpKey] != null)
                serverVariables = "IssueTracker Client Remote Address: " + HttpContext.Current.Request.ServerVariables[serverVariablesIpKey] + Environment.NewLine + (serverVariables ?? "");
            const string serverVariablesHostKey = "REMOTE_HOST";
            if (HttpContext.Current != null && HttpContext.Current.Request.ServerVariables[serverVariablesHostKey] != null)
                serverVariables = "IssueTracker Client Remote Host: " + HttpContext.Current.Request.ServerVariables[serverVariablesHostKey] + Environment.NewLine + (serverVariables ?? "");

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
                        text = text.Substring(text.LastIndexOf("---> ", StringComparison.InvariantCultureIgnoreCase) + 5);
                }
            }

            var parentIssue = _db.Issues.FirstOrDefault(x => x.Text == text && x.StackTrace == stackTrace); //find an identical issue

            // only log the issue if there's not an parent issue that was just posted (prevent overposting)
            if (parentIssue != null && parentIssue.DateOfCreation >= DateTime.Now.AddMinutes(-30))
                return null;

            var newIssue = new Issue
            {
                DateOfCreation = DateTime.Now,
                Creator = Util.MaxLength(creator, 4000),
                Text = Util.MaxLength(text, 4000),
                StackTrace = Util.MaxLength(stackTrace, 8000),
                ServerVariables = Util.MaxLength(serverVariables, 8000),
                Status = Util.MaxLength(IssueTrackerSettings.StatusForNewIssues, 4000),
                AssignedTo = null
            };
            _db.Issues.Add(newIssue);
            _db.SaveChanges();

            new AddTagsService(_db).AddTags(newIssue);

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
                _db.Comments.Add(comment);

                var status = _db.Status.FirstOrDefault(x => x.Name.ToLower() == parentIssue.Status);
                if (status != null && status.Reactivate)
                    parentIssue.Status = Util.MaxLength(IssueTrackerSettings.StatusForNewIssues, 4000);
                else
                    newIssue.Status = parentIssue.Status;
            }

            _db.SaveChanges();
            return newIssue;
        }
    }
}
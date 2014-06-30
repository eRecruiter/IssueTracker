using ePunkt.IssueTracker.Models;
using ePunkt.Utilities;
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
        public Issue Create([CanBeNull]string creator, [CanBeNull]string text, [CanBeNull]string stackTrace, [CanBeNull] string serverVariables, [CanBeNull] string version)
        {
            // get a nicer error message from the stacktrace for generic asp.net errors
            var uglyTexts = new[] {
                    "Exception of type 'System.Web.HttpUnhandledException' was thrown.",
                    "Error executing child request for handler 'System.Web.Mvc.HttpHandlerUtil+ServerExecuteHttpHandlerWrapper'."
                };
            if (text.IsNoW() || uglyTexts.Any(x => x.Is(text)))
            {
                if (stackTrace.HasValue())
                {
                    text = (stackTrace ?? "").Split('\n')[0].Trim();

                    if (text.Contains("---> "))
                        text = text.Substring(text.LastIndexOf("---> ", StringComparison.InvariantCultureIgnoreCase) + 5);
                }
            }

            var parentIssue = _db.Issues.FirstOrDefault(x => x.Text == text && x.StackTrace == stackTrace); // find an identical issue

            // only log the issue if there's not an parent issue that was just posted (prevent overposting)
            if (parentIssue != null && parentIssue.DateOfCreation >= DateTime.Now.AddHours(-1))
                return null;

            string remoteHost = null;
            const string serverVariablesHostKey = "REMOTE_HOST";
            if (HttpContext.Current != null && HttpContext.Current.Request.ServerVariables[serverVariablesHostKey] != null)
                remoteHost = HttpContext.Current.Request.ServerVariables[serverVariablesHostKey];

            var newIssue = new Issue
            {
                DateOfCreation = DateTime.Now,
                Creator = creator,
                Text = text,
                StackTrace = stackTrace,
                ServerVariables = serverVariables,
                Status = IssueTrackerSettings.StatusForNewIssues,
                Version = version,
                RemoteHost = remoteHost,
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
                    Creator = creator,
                    DateOfCreation = DateTime.Now,
                    DuplicateIssueId = newIssue.Id,
                    IssueId = parentIssue.Id,
                    Text = ""
                };
                _db.Comments.Add(comment);

                var status = _db.Status.FirstOrDefault(x => x.Name.ToLower() == parentIssue.Status);
                if (status != null && status.Reactivate)
                    parentIssue.Status = IssueTrackerSettings.StatusForNewIssues;
                else
                    newIssue.Status = parentIssue.Status;
            }

            _db.SaveChanges();
            return newIssue;
        }
    }
}
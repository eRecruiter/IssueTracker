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

        public CreateIssueService([NotNull] Db db)
        {
            _db = db;
        }

        [CanBeNull]
        public Issue Create([CanBeNull] string creator, [CanBeNull] string text, [CanBeNull] string stackTrace, [CanBeNull] string serverVariables, [CanBeNull] string version)
        {
            try
            {
                // sometimes we get some escaped characters
                text = (text ?? "").Replace("---&gt;", "--->");
                stackTrace = (stackTrace ?? "").Replace("---&gt;", "--->");

                // get a nicer error message from the stacktrace for generic asp.net errors (if the text only contains useless info)
                var uglyTexts = new[]
                {
                    "Exception of type 'System.Web.HttpUnhandledException' was thrown.",
                    "Error executing child request for handler 'System.Web.Mvc.HttpHandlerUtil+ServerExecuteHttpHandlerWrapper'."
                };
                if (text.IsNoW() || uglyTexts.Any(x => x.Is(text)))
                {
                    if (stackTrace.HasValue())
                    {
                        text = stackTrace.Split('\n')[0].Trim();

                        if (text.Contains("---> "))
                            text = text.Substring(text.LastIndexOf("---> ", StringComparison.InvariantCultureIgnoreCase) + 5);
                    }
                }

                // remove some useless info from the text to make it easier to read
                if (text.HasValue())
                {
                    text = text.Replace("System.Web.HttpUnhandledException (0x80004005): Exception of type 'System.Web.HttpUnhandledException' was thrown. ---> ", "");
                }

                // find an identical issue
                var parentIssue = _db.Issues
                    .Where(x => x.Text == text || (text == "" && x.Text == null))
                    .Where(x => x.StackTrace == stackTrace || (stackTrace == "" && x.StackTrace == null))
                    .OrderByDescending(x => x.DateOfCreation)
                    .FirstOrDefault();

                // only log the issue if there's not an parent issue that was just posted (prevent overposting)
                if (parentIssue != null && parentIssue.DateOfCreation >= DateTime.Now.ToUniversalTime().AddHours(-1))
                {
                    return null;
                }

                string remoteHost = null;
                const string serverVariablesHostKey = "REMOTE_HOST";
                if (HttpContext.Current != null && HttpContext.Current.Request.ServerVariables[serverVariablesHostKey] != null)
                    remoteHost = HttpContext.Current.Request.ServerVariables[serverVariablesHostKey];

                var newIssue = new Issue
                {
                    DateOfCreation = DateTime.Now.ToUniversalTime(),
                    Creator = creator.GetStringOrNull(),
                    Text = text.GetStringOrNull(),
                    StackTrace = stackTrace.GetStringOrNull(),
                    ServerVariables = serverVariables.GetStringOrNull(),
                    Status = IssueTrackerSettings.StatusForNewIssues,
                    Version = version.GetStringOrNull(),
                    RemoteHost = remoteHost.GetStringOrNull(),
                    AssignedTo = null
                };
                _db.Issues.Add(newIssue);
                _db.SaveChanges();

                if (parentIssue != null)
                {
                    parentIssue = parentIssue.ParentIssueId.HasValue ? _db.Issues.Single(x => x.Id == parentIssue.ParentIssueId.Value) : parentIssue;
                    newIssue.ParentIssueId = parentIssue.Id;

                    var comment = new Comment
                    {
                        Creator = creator,
                        DateOfCreation = DateTime.Now.ToUniversalTime(),
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

                new AddTagsService(_db).AddTags(newIssue);
                return newIssue;
            }
            catch (Exception ex)
            {
                _db.Issues.Add(new Issue
                {
                    DateOfCreation = DateTime.Now.ToUniversalTime(),
                    Creator = "IssueTracker",
                    Text = ex.Message,
                    StackTrace = ex.ToString(),
                    ServerVariables = null,
                    Status = IssueTrackerSettings.StatusForNewIssues,
                    Version = GetType().Assembly.GetName().Version.ToString(),
                    RemoteHost = null,
                    AssignedTo = null
                });
                _db.SaveChanges();
            }
            return null;
        }
    }
}
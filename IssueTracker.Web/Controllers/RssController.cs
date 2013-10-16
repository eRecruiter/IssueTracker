using ePunkt.Utilities;
using IssueTracker.Web.Code;
using IssueTracker.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;

namespace IssueTracker.Web.Controllers
{
    public class RssController : Controller
    {
        public ActionResult Index(string status, string assignedTo)
        {
            var filterText = "";
            if (status.HasValue())
                filterText += "status '" + status + "'";
            if (assignedTo.HasValue())
            {
                if (filterText.HasValue())
                    filterText += " and ";
                filterText += "assigned to '" + assignedTo + "'";
            }
            if (filterText.HasValue())
                filterText = " (filtered by " + filterText + ")";

            var feed = new SyndicationFeed
            {
                BaseUri = new Uri("http://issuetracker.epunkt.net"),
                Generator = "IssueTracker",
                LastUpdatedTime = DateTime.Now,
                Title = new TextSyndicationContent("Issues" + filterText)
            };

            var context = new Db();
            var issues = from x in context.Issues
                         where !x.ParentIssueId.HasValue
                         select x;

            if (status.HasValue())
                issues = issues.Where(x => x.Status == status);

            if (assignedTo.HasValue())
                issues = assignedTo.Is("-") ? issues.Where(x => x.AssignedTo == null) : issues.Where(x => x.AssignedTo == assignedTo);

            var url = Request.Url;
            if (url == null)
                throw new ApplicationException("No 'Request.Url' available.");
            var baseUrl = url.ToString();
            baseUrl = baseUrl.Substring(0, baseUrl.ToLower().IndexOf("/rss", StringComparison.InvariantCultureIgnoreCase));

            var items = new List<SyndicationItem>();
            foreach (var issue in issues)
            {
                var item = new SyndicationItem
                {
                    Title = SyndicationContent.CreatePlaintextContent(issue.Text),
                    Content =
                        SyndicationContent.CreateHtmlContent("<pre>" + issue.StackTrace.Replace("\r", "").Replace("\n", "<br />") + "</pre><hr /><pre>" +
                                                             issue.ServerVariables.Replace("\r", "").Replace("\n", "<br />") + "</pre>"),
                    Id = issue.Id.ToString(CultureInfo.CurrentCulture)
                };
                item.AddPermalink(new Uri(baseUrl + "/Issue/Details/" + issue.Id));
                item.LastUpdatedTime = issue.DateOfUpdate;
                item.PublishDate = issue.DateOfCreation;
                item.Authors.Add(new SyndicationPerson(issue.Creator));

                items.Add(item);
            }
            feed.Items = items;

            return new RssActionResult
            {
                Feed = feed
            };
        }
    }
}

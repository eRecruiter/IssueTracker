using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using IssueTracker.Models;

namespace IssueTracker.Controllers {
    public class RssController : Controller {

        public ActionResult Index(string status, string assignedTo) {
            var filterText = "";
            if (status.HasValue())
                filterText += "status '" + status + "'";
            if (assignedTo.HasValue()) {
                if (filterText.HasValue())
                    filterText += " and ";
                filterText += "assigned to '" + assignedTo + "'";
            }
            if (filterText.HasValue())
                filterText = " (filtered by " + filterText + ")";



            var feed = new SyndicationFeed();
            feed.BaseUri = new Uri("http://issuetracker.epunkt.net");
            feed.Generator = "IssueTracker";
            feed.LastUpdatedTime = DateTime.Now;
            feed.Title = new TextSyndicationContent("Issues" + filterText);

            var context = new Db();
            var issues = from x in context.Issues
                         where !x.ParentIssueId.HasValue
                         select x;

            if (status.HasValue())
                issues = issues.Where(x => x.Status == status);

            if (assignedTo.HasValue())
                if (assignedTo.Is("-"))
                    issues = issues.Where(x => x.AssignedTo == null);
                else
                    issues = issues.Where(x => x.AssignedTo == assignedTo);

            var baseUrl = Request.Url.ToString();
            baseUrl = baseUrl.Substring(0, baseUrl.IndexOf("/Rss"));

            var items = new List<SyndicationItem>();
            foreach (var issue in issues) {
                var item = new SyndicationItem();
                item.Title = SyndicationContent.CreatePlaintextContent(issue.Text);
                item.Content = SyndicationContent.CreateHtmlContent("<pre>" + issue.StackTrace.Replace("\r", "").Replace("\n", "<br />") + "</pre><hr /><pre>" + issue.ServerVariables.Replace("\r", "").Replace("\n", "<br />") + "</pre>");
                item.Id = issue.Id.ToString();
                item.AddPermalink(new Uri(baseUrl + "/Issue/Details/" + issue.Id));
                item.LastUpdatedTime = issue.DateOfUpdate;
                item.PublishDate = issue.DateOfCreation;
                item.Authors.Add(new SyndicationPerson(issue.Creator));

                items.Add(item);
            }
            feed.Items = items;

            return new RssActionResult() {
                Feed = feed
            };
        }

    }
}

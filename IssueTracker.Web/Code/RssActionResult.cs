using System.ServiceModel.Syndication;
using System.Xml;
using System.Web.Mvc;

namespace IssueTracker {
    public class RssActionResult : ActionResult {

        public SyndicationFeed Feed {
            get;
            set;
        }

        public override void ExecuteResult(ControllerContext context) {
            context.HttpContext.Response.ContentType = "application/rss+xml";

            var formatter = new Rss20FeedFormatter(Feed);
            using (var writer = XmlWriter.Create(context.HttpContext.Response.Output)) {
                formatter.WriteTo(writer);
            }
        }

    }
}
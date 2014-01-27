using ePunkt.IssueTracker.Code;
using ePunkt.IssueTracker.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using ApplicationException = System.ApplicationException;

namespace ePunkt.IssueTracker.Controllers
{
    public class HomeController : Controller
    {
        readonly Db _db = new Db();

        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Issues");
        }

        public ActionResult Error()
        {
            throw new ApplicationException("This exception is just a test.");
        }

        [HttpPost, ValidateInput(false)]
        [AllowAnonymous]
        public ActionResult Index(int? parentId, string text, string stackTrace, string serverVariables, string source, string version)
        {
            if (!string.IsNullOrWhiteSpace(version))
                serverVariables = "Version: " + version + Environment.NewLine + (serverVariables ?? "");

            Issue parentIssue = null;
            if (parentId.HasValue)
                parentIssue = _db.Issues.FirstOrDefault(x => x.Id == parentId.Value);

            if (parentIssue != null)
            {
                parentIssue.AddComment(source, text);
                return new JsonResult
                {
                    Data = parentIssue.Id
                };
            }

            var issue = new CreateIssueService(_db).Create(source, text, stackTrace, serverVariables);
            return new JsonResult
            {
                Data = issue == null ? 0 : issue.Id
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}

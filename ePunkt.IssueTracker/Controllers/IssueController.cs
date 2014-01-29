using System.Data.Entity;
using ePunkt.IssueTracker.Code;
using ePunkt.IssueTracker.Models;
using ePunkt.IssueTracker.ViewModels.Issue;
using ePunkt.Utilities;
using System.Linq;
using System.Web.Mvc;

namespace ePunkt.IssueTracker.Controllers
{
    [Authorize]
    public class IssueController : Controller
    {
        private readonly Db _db = new Db();

        public ActionResult Index(int id)
        {
            var issue = _db.Issues.Include(x => x.Tags).Include(x => x.Comments).SingleOrDefault(x => x.Id == id);
            if (issue == null)
                return RedirectToAction("Index", "Issues");
            return View(new IndexViewModel(_db, this.GetCurrentUser(_db), issue, ViewData));
        }

        #region Update / Delete
        [Authorize]
        public ActionResult Update(int id, string status, string user, string text, string notification)
        {
            var updateService = new UpdateIssueService(_db, ViewData);
            updateService.Update(User, id, user, status, text);

            if (notification.HasValue())
                SendNotification(id, notification);

            return RedirectToAction("Index", "Issue", new { id });
        }

        private void SendNotification(int issueId, string email)
        {
            var requestUrl = Request.Url;

            var currentUser = this.GetCurrentUser(_db);
            var body = currentUser.Name + " wants to let you know about changes on issue #" + issueId +
                       (email.IsNoE() ? ". " : ": <br /><br /><i>" + Server.HtmlEncode(email).Replace("\r", "").Replace("\n", "<br />") + "</i><br /><br />") + "Please visit " +
                       (requestUrl == null ? "" : requestUrl.ToString().Replace("Update", "Index")) + " for all the details.</div><br /><br />Have a nice day! Yours,<br />~ the IssueTracker E-Mail-Monkey";

            var fromEmail = currentUser.Email;
            if (string.IsNullOrEmpty(fromEmail))
                fromEmail = "somebody@somewhere.com";

            Utils.SendMail(currentUser.Name, fromEmail, email, email, "Info from " + currentUser.Name + " about issue #" + issueId, body);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var deleteService = new DeleteIssueService(_db);
            deleteService.Delete(id);
            return RedirectToAction("Index", "Issues");
        }
        #endregion
    }
}

using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using IssueTracker.Models;
using IssueTracker.ViewModels.Home;


namespace IssueTracker.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {
            return RedirectToAction("Index", "Issue");
        }


        [HttpPost, ValidateInput(false)]

        public ActionResult Index(int? parentId, string text, string stackTrace, string serverVariables, string source, string version) {
            if (!string.IsNullOrWhiteSpace(version))
                serverVariables = "Version: " + version + Environment.NewLine + (serverVariables ?? "");

            Issue parentIssue = null;
            if (parentId.HasValue)
                using (var context = new Db())
                    parentIssue = context.Issues.FirstOrDefault(x => x.Id == parentId.Value);

            if (parentIssue != null) {
                parentIssue.AddComment(source, text);
                return new JsonResult {
                    Data = parentIssue.Id
                };
            }

            return new JsonResult {
                Data = Issue.Create(source, text, stackTrace, serverVariables).Id
            };
        }


        #region LogOn / LogOff
        public ActionResult LogOff() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }


        public ActionResult LogOn() {
            return View(new LogOnViewModel());
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogOn(string username, string password) {
            using (var context = new Db()) {
                var user = context.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password.Hash());
                if (user != null) {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    return RedirectToAction("Index", "Issue");
                }
            }
            return View(new LogOnViewModel {
                Message = "Invalid username or password."
            });
        }
        #endregion


    }
}

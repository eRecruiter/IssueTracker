using System.Linq;
using IssueTracker.Web.Models;
using IssueTracker.Web.Models.ViewModels.Account;
using System.Web.Mvc;
using System.Web.Security;

namespace IssueTracker.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            var viewModel = new LoginViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            using (var db = new Db())
            {
                var matchingUser = db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
                if (matchingUser != null)
                    FormsAuthentication.RedirectFromLoginPage(matchingUser.Username, true);
            }

            var viewModel = new LoginViewModel
            {
                Message = "Invalid username or password."
            };
            return View(viewModel);
        }

        public ActionResult Logoff()
        {
            return RedirectToAction("Logout");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/");
        }

    }
}

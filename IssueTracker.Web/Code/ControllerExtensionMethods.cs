using System.Linq;
using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker {
    public static class ControllerExtensionMethods {

        public static User GetCurrentUser(this Controller controller, Db db) {
            return Utils.GetCurrentUser(db, controller.ViewData, controller.HttpContext.User);
        }

    }
}
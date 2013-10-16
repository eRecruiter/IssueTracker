using System.Web.Mvc;
using IssueTracker.Web.Models;

namespace IssueTracker.Web.Code {
    public static class ControllerExtensionMethods {

        public static User GetCurrentUser(this Controller controller, Db db) {
            return Utils.GetCurrentUser(db, controller.ViewData, controller.HttpContext.User);
        }

    }
}
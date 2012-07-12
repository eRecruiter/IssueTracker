using System.Linq;
using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker {
    public static class ControllerExtensionMethods {

        public static User GetCurrentUser(this Controller controller) {
            return Utils.GetCurrentUser(controller.ViewData, controller.HttpContext.User);
        }

    }
}
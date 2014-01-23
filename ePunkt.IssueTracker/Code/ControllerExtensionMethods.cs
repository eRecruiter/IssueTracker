using System.Web.Mvc;
using ePunkt.IssueTracker.Models;

namespace ePunkt.IssueTracker.Code
{
    public static class ControllerExtensionMethods
    {
        public static User GetCurrentUser(this Controller controller, Db db)
        {
            return Utils.GetCurrentUser(db, controller.ViewData, controller.HttpContext.User);
        }
    }
}
using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker {
    public static class ViewExtensionMethods {

        public static User GetCurrentUser(this ViewUserControl view) {
            return Utils.GetCurrentUser(view.ViewData, view.Page.User);
        }

        public static User GetCurrentUser(this ViewPage view) {
            return Utils.GetCurrentUser(view.ViewData, view.Page.User);
        }

    }
}
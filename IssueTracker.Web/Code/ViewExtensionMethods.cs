using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker {
    public static class ViewExtensionMethods {

        public static User GetCurrentUser(this ViewUserControl view, Db db) {
            return Utils.GetCurrentUser(db, view.ViewData, view.Page.User);
        }

        public static User GetCurrentUser(this ViewPage view, Db db) {
            return Utils.GetCurrentUser(db, view.ViewData, view.Page.User);
        }

    }
}
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker {
    public static class Utils {

        public static User GetCurrentUser(ViewDataDictionary viewData, IPrincipal user) {
            if (viewData["user"] == null && user.Identity.IsAuthenticated)
                using (var context = new Db())
                    viewData["user"] = context.Users.FirstOrDefault(x => x.Username.ToLower() == user.Identity.Name.ToLower());
            return viewData["user"] as User;
        }

        public static IEnumerable<User> GetAllUsers(ViewDataDictionary viewData) {
            if (viewData["users"] == null)
                using (var context = new Db())
                    viewData["users"] = context.Users.OrderBy(x => x.Name).ToList();
            return viewData["users"] as IEnumerable<User>;
        }

        public static IEnumerable<Status> GetAllStati(ViewDataDictionary viewData) {
            if (viewData["stati"] == null)
                using (var context = new Db())
                    viewData["stati"] = context.Status.OrderBy(x => x.Name).ToList();
            return viewData["stati"] as IEnumerable<Status>;
        }

    }
}
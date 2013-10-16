using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using IssueTracker.Web.Models;

namespace IssueTracker.Web.Code {
    public static class Utils {

        public static User GetCurrentUser(Db db, ViewDataDictionary viewData, IPrincipal user) {
            if (viewData["user"] == null && user.Identity.IsAuthenticated) {
                var userinDatabase = db.Users.FirstOrDefault(x => x.Username.ToLower() == user.Identity.Name.ToLower());

                if (userinDatabase == null) {
                    userinDatabase = new User {
                        Email = "",
                        Name = user.Identity.Name,
                        Username = user.Identity.Name
                    };

                    db.Users.Add(userinDatabase);
                    db.SaveChanges();
                }

                viewData["user"] = userinDatabase;
            }

            return viewData["user"] as User;
        }

        public static IEnumerable<User> GetAllUsers(Db db, ViewDataDictionary viewData) {
            if (viewData["users"] == null)
                    viewData["users"] = db.Users.OrderBy(x => x.Name).ToList();
            return viewData["users"] as IEnumerable<User>;
        }

        public static IEnumerable<Status> GetAllStati(Db db, ViewDataDictionary viewData) {
            if (viewData["stati"] == null)
                    viewData["stati"] = db.Status.OrderBy(x => x.Name).ToList();
            return viewData["stati"] as IEnumerable<Status>;
        }

    }
}
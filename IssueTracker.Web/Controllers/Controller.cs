using System.Linq;
using System.Web.Mvc;
using IssueTracker.Web.Models;

namespace IssueTracker.Web.Controllers {
    public class Controller : System.Web.Mvc.Controller {

        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            using (var context = new Db()) {
                ViewData["availableStati"] = context.Status.Select(x => x.Name).OrderBy(x => x).ToList();
            }
        }

    }
}
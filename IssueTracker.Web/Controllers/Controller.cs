using System.Linq;
using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker.Controllers {
    public class Controller : System.Web.Mvc.Controller {

        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            using (var context = new Db()) {
                ViewData["availableStati"] = context.Status.Select(x => x.Name).OrderBy(x => x).ToList();
            }
        }

    }
}
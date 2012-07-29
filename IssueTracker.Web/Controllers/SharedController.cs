using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker.Web.Controllers {
    [Authorize]
    public class SharedController : Controller {

        private Db _db = new Db();

        [ChildActionOnly]
        public ActionResult Header() {
            return PartialView(this.GetCurrentUser(_db));
        }

    }
}

using System.Web.Mvc;
using IssueTracker.Web.Code;
using IssueTracker.Web.Models;

namespace IssueTracker.Web.Controllers {
    public class SharedController : Controller {

        private readonly Db _db = new Db();

        [ChildActionOnly]
        public ActionResult Header() {
            return PartialView(this.GetCurrentUser(_db));
        }

    }
}

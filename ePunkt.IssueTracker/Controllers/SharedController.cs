using ePunkt.IssueTracker.Code;
using ePunkt.IssueTracker.Models;
using ePunkt.IssueTracker.ViewModels.Shared;
using System.Web.Mvc;

namespace ePunkt.IssueTracker.Controllers
{
    public class SharedController : Controller
    {
        private readonly Db _db = new Db();

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView(new HeaderViewModel(_db, ViewData).Fill(User, new UserOptions(Request.Cookies, Response.Cookies)));
        }

        [ChildActionOnly]
        public ActionResult TagFilter()
        {
            var userOptions = new UserOptions(Request.Cookies, Response.Cookies);
            var viewModel = new TagFilterViewModel(_db, userOptions);
            return PartialView(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}

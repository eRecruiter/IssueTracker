using ePunkt.IssueTracker.Web.Code;
using ePunkt.IssueTracker.Web.Models;
using ePunkt.IssueTracker.Web.ViewModels.Shared;
using System.Web.Mvc;

namespace ePunkt.IssueTracker.Web.Controllers
{
    public class SharedController : Controller
    {
        private readonly Db _db = new Db();

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView(new HeaderViewModel(_db, ViewData).Fill(User, new UserOptions(Request.Cookies, Response.Cookies)));
        }
    }
}

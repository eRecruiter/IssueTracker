using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IssueTracker.Models;

namespace IssueTracker.Web.Controllers
{
    public class SharedController : Controller
    {

        private Db _db = new Db();

        [ChildActionOnly]
        public ActionResult Header()
        {
            return View(this.GetCurrentUser(_db));
        }

    }
}

using ePunkt.IssueTracker.Web.App_Start;
using ePunkt.IssueTracker.Web.Migrations;
using ePunkt.IssueTracker.Web.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ePunkt.IssueTracker.Web {
    public class MvcApplication : System.Web.HttpApplication {

        protected void Application_Start() {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Db, Configuration>());

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
using IssueTracker.Web.App_Start;
using IssueTracker.Web.Migrations;
using IssueTracker.Web.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IssueTracker.Web {
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
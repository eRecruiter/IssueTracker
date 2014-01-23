using ePunkt.IssueTracker.App_Start;
using ePunkt.IssueTracker.Migrations;
using ePunkt.IssueTracker.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ePunkt.IssueTracker {
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
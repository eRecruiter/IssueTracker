using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CloudPass.Web;
using IssueTracker.Models;
using IssueTracker.Web.Migrations;


namespace ComicSyndicate.Web {
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
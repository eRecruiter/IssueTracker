using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AppfailReporting.Mvc;


namespace ComicSyndicate.Web {

    public class MvcApplication : System.Web.HttpApplication {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new AppfailReportAttribute()); 
            filters.Add(new HandleErrorAttribute());
        }


        protected void Application_Start() {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<Db, Configuration>());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.EnableDefaultBundles();
        }


        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        }

    }
}
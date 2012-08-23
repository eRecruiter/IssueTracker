using System.Web.Optimization;

namespace CloudPass.Web {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/js/jquery").Include(
                "~/Scripts/jquery-1.8.0.js"));
            bundles.Add(new ScriptBundle("~/js/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/css/bootstrap-responsive").Include(
                "~/Content/bootstrap-responsive.css"));
        }
    }
}
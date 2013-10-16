using System.Web.Optimization;

namespace CloudPass.Web {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js", "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/css/css").Include(
                "~/Content/bootstrap/bootstrap.css", "~/Content/bootstrap/bootstrap-theme.css", "~/Content/site.css"));
        }
    }
}
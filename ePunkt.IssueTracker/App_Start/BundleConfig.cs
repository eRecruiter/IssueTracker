using System.Web.Optimization;

namespace ePunkt.IssueTracker.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js", "~/Scripts/bootstrap.js", "~/Scripts/mousetrap-{version}.js", "~/Scripts/mousetrap-global-bind.js"));

            bundles.Add(new StyleBundle("~/css/css").Include(
                "~/Content/bootstrap.css", "~/Content/site.css"));
        }
    }
}
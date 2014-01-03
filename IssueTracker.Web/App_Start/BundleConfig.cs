using System.Web.Optimization;

namespace ePunkt.IssueTracker.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js", "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/css/css").Include(
                "~/Content/bootstrap.css", "~/Content/site.css"));
        }
    }
}
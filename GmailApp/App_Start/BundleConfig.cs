using System.Web;
using System.Web.Optimization;

namespace GmailApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            /// JQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.min.js"));

            /// Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/bootstrap.min.js"));
            bundles.Add(new StyleBundle("~/bundles/bootstrapcss").Include(
                        "~/Content/bootstrap.min.css"));

            /// Summernote
            bundles.Add(new ScriptBundle("~/bundles/summernotejs").Include(
                        "~/Scripts/summernote.min.js"));
            bundles.Add(new StyleBundle("~/bundles/summernotecss").Include(
                        "~/Content/summernote.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
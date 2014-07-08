using System.Diagnostics;
using System.Web;
using System.Web.Optimization;

namespace EProductivity.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/js/plugins")
                .IncludeDirectory("~/Scripts/plugins","*.js",true));
            bundles.Add(new ScriptBundle("~/js/base")
                .Include("~/Scripts/general.js"));
            
            bundles.Add(new ScriptBundle("~/js/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/js/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/js/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/css/base").Include(
                      "~/Content/bootstrap.css",
                      "~/fonts/font-awesome-4/css/font-awesome.css"));

            bundles.Add(new StyleBundle("~/css/template").Include(
                      "~/Content/pygments.css",
                      "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/css/plugins").IncludeDirectory(
                      "~/Scripts/plugins","*.css",true));
            

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = !Debugger.IsAttached;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace ClaimManagementSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery/jquery-{version}.js",
                "~/Scripts/jquery/jquery.redirect.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery/jquery.unobtrusive*",
                "~/Scripts/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/Angular/angular.min.js",
                "~/Scripts/Angular/angular-animate.min.js",
                "~/Scripts/Angular/angular-resource.min.js",
                "~/Scripts/Angular/angular-sanitize.min.js",
                "~/Scripts/Angular/angular-route.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
               "~/Scripts/kendo/kendo.all.min.js",
                // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
               "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo/angular").Include(
                "~/Scripts/kendo/kendo.angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/home").Include(
                "~/Scripts/app/Home/home-controller.js",
                "~/Scripts/app/Home/home-directive.js",
                "~/Scripts/app/Home/home-service.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/claims").Include(
                "~/Scripts/app/Claims/claim-service.js",
                "~/Scripts/app/Claims/claim-directive.js",
                "~/Scripts/app/Claims/claim-controller.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/Site/StyleSheets/Bootstrap/bootstrap.min.css",
                 "~/Content/Site/StyleSheets/Font.css",
                 "~/Content/Site/StyleSheets/Site.css",
                 "~/Content/Site/StyleSheets/Custom.css"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
              "~/Content/kendo/kendo.common-bootstrap.min.css",
              "~/Content/kendo/kendo.common-min.css",
              "~/Content/kendo/kendo.bootstrap.min.css",
              "~/Content/kendo/kendo.blueopel.min.css",
              "~/Content/kendo/kendo.custom.css",
              "~/Content/kendo/kendo.custom.less"
              ));
            bundles.IgnoreList.Clear();

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}

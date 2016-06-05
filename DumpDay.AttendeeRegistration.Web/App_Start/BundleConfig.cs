using System.Web.Optimization;

namespace DumpDay.AttendeeRegistration.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/Vendor/angular.js",
                "~/Scripts/Vendor/angular-route.js",
                "~/Scripts/Vendor/js-enumeration.js",
                "~/Scripts/Vendor/saveAs.js",

                "~/Scripts/App/app.module.js",
                "~/Scripts/App/app.config.js",

                "~/Scripts/App/common/constants/workStatuses.constant.js",
                "~/Scripts/App/common/directives/confirm.directive.js",
                "~/Scripts/App/common/services/print.service.js",

                "~/Scripts/App/attendee/controllers/attendee.controller.js",
                "~/Scripts/App/attendee/controllers/attendee.create.controller.js",
                "~/Scripts/App/attendee/controllers/attendee.delete.controller.js",
                "~/Scripts/App/attendee/controllers/attendee.list.controller.js",
                "~/Scripts/App/attendee/controllers/attendee.print.controller.js",

                "~/Scripts/App/attendee/services/attendee.service.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/google-fonts",
                @"https://fonts.googleapis.com/css?family=Roboto:400,400italic,700,300&subset=latin,latin-ext")
            );

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/css/*.css",
                new CssRewriteUrlTransform()
            ));
        }
    }
}
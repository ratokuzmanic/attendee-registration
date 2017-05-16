using System.Web.Optimization;

namespace DumpDays.AttendeeRegistration.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.UseCdn = true;

            bundles.Add(new StyleBundle("~/bundles/googleFonts",
                @"https://fonts.googleapis.com/css?family=Asap:400,400i,500,500i,700,700i")
            );
            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/css/*.css",
                new CssRewriteUrlTransform()
            ));

            bundles.Add(new ScriptBundle("~/bundles/minifiedScripts").Include(
                "~/Scripts/Vendor/angular.js",
                "~/Scripts/Vendor/angular-clipboard.js",
                "~/Scripts/Vendor/ui-router.js",
                "~/Scripts/Vendor/Chart.js",
                "~/Scripts/Vendor/angular-chart.js",
                "~/Scripts/Vendor/js-enumeration.js",
                "~/Scripts/Vendor/jwt-decode.js",
                "~/Scripts/Vendor/saveAs.js",
                "~/Scripts/Vendor/underscore.js",
                "~/Scripts/Vendor/webcam.js",

                "~/Scripts/App/app.module.js",
                "~/Scripts/App/app.config.js",

                "~/Scripts/App/common/constants/roles.constant.js",
                "~/Scripts/App/common/constants/workStatuses.constant.js",
                "~/Scripts/App/common/directives/confirmDelete.directive.js",
                "~/Scripts/App/common/interceptors/auth.interceptor.js",
                "~/Scripts/App/common/navigation/controller.js",
                "~/Scripts/App/common/services/print.service.js",

                "~/Scripts/App/attendee/common/service.js",
                "~/Scripts/App/attendee/create/controller.js",
                "~/Scripts/App/attendee/delete/controller.js",
                "~/Scripts/App/attendee/list/controller.js",
                "~/Scripts/App/attendee/print/controller.js",
                "~/Scripts/App/attendee/scan/controller.js",
                "~/Scripts/App/attendee/statistics/constant.js",
                "~/Scripts/App/attendee/statistics/controller.js",
                "~/Scripts/App/attendee/statistics/service.js",

                "~/Scripts/App/auth/common/service.js",
                "~/Scripts/App/auth/login/controller.js",

                "~/Scripts/App/user/common/service.js",
                "~/Scripts/App/user/manage/controller.js",
                "~/Scripts/App/user/setup/controller.js"
            ));

            var unminifiedBundle = new ScriptBundle("~/bundles/unminifiedScripts").Include(
                "~/Scripts/Vendor/bc-qr-reader.js",
                "~/Scripts/App/attendee/create/state.js",
                "~/Scripts/App/attendee/delete/state.js",
                "~/Scripts/App/attendee/list/state.js",
                "~/Scripts/App/attendee/print/state.js",
                "~/Scripts/App/attendee/scan/state.js",
                "~/Scripts/App/attendee/statistics/state.js",
                "~/Scripts/App/auth/login/state.js",
                "~/Scripts/App/user/manage/state.js",
                "~/Scripts/App/user/setup/state.js"
            );
            unminifiedBundle.Transforms.Clear();
            bundles.Add(unminifiedBundle);
        }
    }
}
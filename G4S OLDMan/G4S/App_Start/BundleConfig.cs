using System.Web.Optimization;

namespace G4S
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/app/app.js",
                "~/app/config.js",
#if DEBUG
        "~/app/appconfig.debug.js",
#else 
        "~/app/appconfig.release.js",
#endif
                "~/app/device/*.js",
                "~/app/directives/*.js",
                "~/app/modals/*.js",
                "~/app/user/*.js",
                "~/app/userrole/*.js",
                "~/app/userrolegroup/*.js",
                "~/app/directives/*.js",
                "~/app/login/*.js",
                "~/app/services/*.js",
                "~/app/import/*.js",
                "~/app/loginSite/*.js",
                "~/app/loginLicence/*.js",
                "~/app/flocId/*.js",
                "~/app/deviceType/*.js",
                "~/app/productType/*.js",
                "~/app/toBeTreatedDevice/*.js",
                "~/app/toBeTreatedLwpSetting/*.js",
                "~/app/deviceStateHistory/*.js",
                "~/app/orderStateHistory/*.js",
                "~/app/navbar/*.js",
                "~/app/language/*.js",
                "~/app/translation/*.js",
                "~/app/state/*.js",
                "~/app/stateChange/*.js",
                "~/app/orderStateChange/*.js",
                "~/app/settings/*.js",
                "~/app/lwpSetting/*.js",
                "~/app/repairReason/*.js",
                "~/app/purchaseOrder/*.js",
                "~/app/orderItem/*.js",
                "~/app/dashboard/*.js",
                "~/app/platform/*.js"


            ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-ui-router.min.js",
                "~/Scripts/angular-animate.min.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                "~/Scripts/angular-cookies.min.js",
                "~/Scripts/angular-translate.min.js",
                "~/Scripts/angular-translate-loader-url.min.js",
                "~/Scripts/angular-mocks.js",
                "~/Scripts/filesaver.min.js",
                "~/Scripts/linq.min.js",
                "~/Scripts/ng-file-upload-shim.min.js",
                "~/Scripts/ng-file-upload.min.js",
                "~/bower_components/angular-smart-table/dist/smart-table.min.js",
                "~/bower_components/tinycolor/dist/tinycolor-min.js",
                "~/bower_components/angular-color-picker/dist/angularjs-color-picker.min.js",
                "~/bower_components/moment/moment.js",
                "~/bower_components/moment-timezone/moment-timezone.js",
                "~/Scripts/toaster.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));



            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap/css/bootstrap.min.css",
                 "~/bower_components/tink-core/dist/tink.css",
                 "~/bower_components/angular-color-picker/dist/angularjs-color-picker.min.css",
                 "~/bower_components/angular-color-picker/dist/themes/angularjs-color-picker-bootstrap.min.css",
                 "~/Content/css/waves.min.css",
                 "~/Content/css/nanoscroller.css",
                 "~/Content/css/morris-0.4.3.min.css",
                 "~/Content/css/menu-light.css",
                 "~/Content/css/style.css",
                 "~/Content/font-awesome/css/font-awesome.min.css",
                 "~/Content/css/app.min.1.css",
                 "~/Content/css/fullcalendar.min.css",
                 "~/Content/css/themify-icons.css",
                 "~/Content/css/color.css",
                 "~/Content/toaster.min.css",
                 "~/Content/Site.css"));
        }
    }
}

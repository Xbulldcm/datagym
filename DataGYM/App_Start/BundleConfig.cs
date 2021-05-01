using System.Web;
using System.Web.Optimization;

namespace DataGYM
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include( //CSS de la aplicacion
                        "~/Content/bootstrap.css",
                        "~/adminlte/css/adminlte.min.css",
                        "~/adminlte/plugins/fontawesome-free/css/all.min.css",
                        "~/adminlte/plugins/select2/css/select2.min.css",
                        "~/adminlte/plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css",
                        "~/adminlte/plugins/bootstrap4-duallistbox/bootstrap-duallistbox.min.css",
                        "~/adminlte/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                        "~/adminlte/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
                        "~/adminlte/plugins/pace-progress/themes/black/pace-theme-flat-top.css",
                        "~/adminlte/plugins/bootstrap4-duallistbox/bootstrap-duallistbox.min.css",
                        "~/adminlte/plugins/bs-stepper/css/bs-stepper.min.css",
                        "~/adminlte/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                        "~/adminlte/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                        "~/adminlte/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                        "~/adminlte/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",
                        "~/adminlte/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                        "~/adminlte/plugins/summernote/summernote-bs4.min.css",
                        "~/adminlte/plugins/fullcalendar/main.min.css",
                        "~/adminlte/plugins/fullcalendar-daygrid/main.min.css",
                        "~/adminlte/css/docs.css",
                        "~/adminlte/plugins/fullcalendar-bootstrap/main.min.css"
                        ));

            bundles.Add(new ScriptBundle("~/adminlte/js").Include( //JS de la aplicacion
                        "~/adminlte/js/adminlte.min.js",
                        "~/adminlte/js/demo.js",
                        "~/adminlte/plugins/pace-progress/pace.min.js",
                        "~/adminlte/plugins/custom/layout.js",
                        "~/adminlte/plugins/custom/routine_exercises.js",
                        "~/adminlte/plugins/custom/dark-mode-switch.js",
                        "~/adminlte/plugins/summernote/summernote-bs4.min.js",
                        "~/adminlte/plugins/moment/moment.min.js",
                        "~/adminlte/plugins/jquery-mousewheel/jquery.mousewheel.js",
                        "~/adminlte/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                        "~/adminlte/plugins/jquery-mousewheel/jquery.mousewheel.js",
                        "~/adminlte/plugins/raphael/raphael.min.js",
                        "~/adminlte/plugins/jquery-mapael/jquery.mapael.min.js",
                        "~/adminlte/plugins/jquery-mapael/maps/usa_states.min.js",
                        "~/adminlte/plugins/datatables/jquery.dataTables.min.js",
                        "~/adminlte/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                        "~/adminlte/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                        "~/adminlte/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
                        "~/adminlte/plugins/datatables-buttons/js/dataTables.buttons.min.js",
                        "~/adminlte/plugins/datatables-buttons/js/buttons.bootstrap4.min.js",
                        "~/adminlte/plugins/jszip/jszip.min.js",
                        "~/adminlte/plugins/pdfmake/pdfmake.min.js",
                        "~/adminlte/plugins/pdfmake/vfs_fonts.js",
                        "~/adminlte/plugins/datatables-buttons/js/buttons.html5.min.js",
                        "~/adminlte/plugins/datatables-buttons/js/buttons.print.min.js",
                        "~/adminlte/plugins/datatables-buttons/js/buttons.colVis.min.js",
                        "~/adminlte/plugins/custom/datatable.js",
                        "~/adminlte/plugins/fullcalendar/main.js",
                        "~/adminlte/plugins/fullcalendar-daygrid/main.js",
                        "~/adminlte/plugins/select2/js/select2.full.min.js",
                        "~/adminlte/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                        "~/adminlte/plugins/bs-stepper/js/bs-stepper.min.js",
                        "~/adminlte/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
                        "~/adminlte/plugins/bootstrap4-duallistbox/jquery.bootstrap-duallistbox.min.js",
                        "~/adminlte/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                        "~/adminlte/plugins/fullcalendar-bootstrap/main.js"
                        ));
            
        }
        
    }
}

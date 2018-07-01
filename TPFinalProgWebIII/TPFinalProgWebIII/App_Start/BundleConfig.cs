using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace TPFinalProgWebIII.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                 "~/Content/js/javascript/jquery*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                     "~/Content/js/javascript/modernizr-2.*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/javascript/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                      "~/Content/css/bootstrap/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/tareaGeneral").Include(
                     "~/Content/js/tareaGeneral.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery110").Include(
                    "~/Content/js/javascript/jquery-1.10.2.min.js"));
          
            bundles.Add(new StyleBundle("~/Content/general").Include(
                "~/Content/css/general.css"));

            bundles.Add(new StyleBundle("~/Content/Carpetas").Include(
                "~/Content/css/Carpetas.css"));

            bundles.Add(new StyleBundle("~/Content/Login").Include(
              "~/Content/css/Login.css"));

            bundles.Add(new StyleBundle("~/Content/Home").Include(
               "~/Content/css/Home.css"));
         
            bundles.Add(new StyleBundle("~/Content/HomeLogueado").Include(
               "~/Content/css/HomeLogueado.css"));

            bundles.Add(new StyleBundle("~/Content/Tareas").Include(
               "~/Content/css/Tareas.css"));

            bundles.Add(new ScriptBundle("~/bundles/validate").Include(
                  "~/Content/js/javascript/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive").Include(
                  "~/Content/js/javascript/jquery.validate.unobtrusive.min.js"));


            BundleTable.EnableOptimizations = true;
        }
    }
}
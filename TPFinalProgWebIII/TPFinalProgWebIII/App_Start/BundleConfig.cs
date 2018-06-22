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
                 "~/Content/js/jquery*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                 "~/Content/js/modernizr-2.*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                      "~/Content/css/bootstrap.css"));


            bundles.Add(new ScriptBundle("~/bundles/touchspin").Include(
                      "~/Content/bootstrap-touchspin-master/src/jquery.bootstrap-touchspin.js"));

            bundles.Add(new ScriptBundle("~/bundles/tareaGeneral").Include(
                     "~/Content/js/tareaGeneral.js"));

            bundles.Add(new ScriptBundle("~/bundles/tareaCrear").Include(
                    "~/Content/js/tareaCrear.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
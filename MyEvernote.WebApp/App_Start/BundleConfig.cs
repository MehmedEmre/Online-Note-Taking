using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MyEvernote.WebApp.App_Start
{
    public class BundleConfig
    {
        public static void  RegisterBundles(BundleCollection Bundle)
        {

            Bundle.Add(new StyleBundle("~/css/all")
                .Include("~/css/all.min.css",
                         "~/Content/shop-homepage.css",
                         "~/Content/bootstrap.min.css",
                         "~/Content/Site.css"));


            Bundle.Add(new ScriptBundle("~/js/all")
            .Include( "~/js/all.min.js",
                       "~/Scripts/jquery-3.4.1.min.js",
                       "~/Scripts/bootstrap.min.js",
                       "~/Scripts/angular.min.js"
                     ));

            Bundle.Add(new ScriptBundle("~/js/app").Include("~/Scripts/Angular/app.js"));

            BundleTable.EnableOptimizations = true;

        }


    }
}
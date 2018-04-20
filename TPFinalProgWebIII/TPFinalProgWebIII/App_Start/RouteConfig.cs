using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TPFinalProgWebIII
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Ruta para tareas..
            routes.MapRoute(
                name: "Tareas",
                url: "tareas/{action}/{id}",
                defaults: new { controller = "Tareas", action = "Index", id = UrlParameter.Optional }
            );

            //Ruta para carpetas..
            routes.MapRoute(
                name: "Carpetas",
                url: "carpetas/{action}/{id}",
                defaults: new { controller = "Carpetas", action = "Index", id = UrlParameter.Optional }
            );

            //Ruta para el Home, sacar la id luego..
            routes.MapRoute(
                name: "Home",
                url: "{controller}/{action}/",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}

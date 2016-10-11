using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCExcerciseKLA
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Empty",
               url: "",
               defaults: new { controller = "Calculator", action = "Show", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Calculate",
               url: "Calculate",
               defaults: new { controller = "Calculator", action = "Calculate", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

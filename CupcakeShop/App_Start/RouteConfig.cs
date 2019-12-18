using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CupcakeShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("FileUpload",
                           "cupcake/uploadfile/{id}",
                           new { controller = "Cupcake", action = "UploadFile" },
                           new[] { "Nop.Web.Controllers" });

            routes.MapRoute("ViewCupcake",
                          "cupcake/Edit/{id}",
                          new { controller = "Cupcake", action = "Edit" },
                          new[] { "Nop.Web.Controllers" });
        }
    }
}
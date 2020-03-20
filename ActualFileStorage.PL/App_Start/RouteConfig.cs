using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ActualFileStorage.PL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DownloadFileData",
                url: "download/{fileUrl}",
                defaults: new { controller = "Profile", action = "DownloadFile" },
                constraints: new { fileUrl = @"\w{32}" }
            );

            routes.MapRoute(
                name: "ShortLink",
                url: "s/{id}",
                defaults: new { controller = "Shortener", action = "Unpack", id = UrlParameter.Optional }//,
                //constraints: new { id = @"\w{12}" }
            );

            //routes.MapRoute(
            //    name: "InvalidShortLink",
            //    url: "s/{id}",
            //    defaults: new { controller = "Home", action = "Index"}
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

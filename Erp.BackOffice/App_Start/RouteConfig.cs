using Erp.BackOffice.Models;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Erp.BackOffice
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.RouteExistingFiles = true;
            //routes.IgnoreRoute("favicon.ico");
            //routes.IgnoreRoute("assets/{*pathInfo}");
            //routes.IgnoreRoute("Scripts/{*pathInfo}");
            //routes.IgnoreRoute("ReportViewer/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.Add("ImagesRoute", new Route("Data/HinhSV/{filename}", new Erp.BackOffice.Filters.ImageRouteHandler()));            
           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Erp.BackOffice.Controllers"}
            );
            RegisterAutoMapperMap();
        }

        public static void RegisterAutoMapperMap()
        {
            
        }
    }
}
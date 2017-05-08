using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Spring.Net.Example
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "adminlogin",
            //    url: "Admin/Account/Login",
            //    defaults: new { controller = "Account", action = "Login" },
            //    namespaces: new string[] { "Spring.Net.Example.Admin.Controllers" }
            //);

            routes.MapRoute(
name: "Admin",
url: "Admin/{controller}/{action}/{id}",
defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
namespaces: new string[] { "Spring.Net.Example.Admin.Controllers" }
);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {   controller = "Home", action = "Index", id = UrlParameter.Optional }
                //namespaces: new string[] { "Spring.Net.Example.Controllers" }
            );


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Spring.Net.Example
{
    using Server.AppStart;
    public class MvcApplication : Spring.Web.Mvc.SpringMvcApplication //System.Web.HttpApplication//
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override void Application_BeginRequest(object sender, EventArgs e)
        {
            //var resolver = BuildDependencyResolver();
            //RegisterDependencyResolver(resolver);
            base.Application_BeginRequest(sender, e);

            var startModel = new Model.Dto.Shared.AppStartParams()
            {
                RootPath = Server.MapPath(""),
                WebAppName = "SpringNetExample"
            };
            startModel.DtoAssemblys.Add("Spring.Net.Example.Model");

            RegisterFrameWork.Register(startModel);

        }
    }
}

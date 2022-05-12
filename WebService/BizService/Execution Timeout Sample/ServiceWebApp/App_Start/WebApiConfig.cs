using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ServiceWebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            TheOne.ServiceModel.Biz.FoxBizServiceConfig.Configure();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "FoxBizService",
                routeTemplate: "api/bizservice/{action}",
                defaults: new { controller = "FoxBizService" }
            );
        }
    }
}

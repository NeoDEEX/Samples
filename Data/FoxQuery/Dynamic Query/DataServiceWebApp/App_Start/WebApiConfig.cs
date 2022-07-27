using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DataServiceWebApp
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
                name: "FoxDataService",
                routeTemplate: "api/dataservice/{action}",
                defaults: new { controller = "FoxDataService" }
            );
        }
    }
}

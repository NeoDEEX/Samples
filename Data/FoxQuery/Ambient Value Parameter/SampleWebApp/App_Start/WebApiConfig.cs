using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SampleWebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Fox Data Service route setting
            config.Routes.MapHttpRoute(
                name: "FoxDataServce",
                routeTemplate: "api/dataservice/{action}",
                defaults: new { controller = "FoxDataService" }
            );
        }
    }
}

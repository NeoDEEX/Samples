using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DataServiceWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Fox Biz Service 라우팅 설정
            config.Routes.MapHttpRoute(
                name: "FoxBizService",
                routeTemplate: "api/bizservice/{action}",
                defaults: new { controller = "FoxBizService" }
            );
            // Fox Data Service 라우팅 설정
            config.Routes.MapHttpRoute(
                name: "FoxDataService",
                routeTemplate: "api/dataservice/{action}",
                defaults: new { controller = "FoxDataService" }
            );
        }
    }
}

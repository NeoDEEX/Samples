using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RealGridApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // Fox Data Service 라우팅 설정
            config.Routes.MapHttpRoute(
                name: "FoxDataService",
                routeTemplate: "api/dataservice/{action}",
                defaults: new { controller = "FoxDataService" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

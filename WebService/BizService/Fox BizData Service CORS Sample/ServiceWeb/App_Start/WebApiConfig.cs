using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ServiceWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // CORS 기능 활성화
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // CORS를 지원하지 않는 Fox Biz Service 컨트롤러 라우팅
            config.Routes.MapHttpRoute(
                name: "FoxBizService",
                routeTemplate: "api/bizservice/{action}",
                defaults: new { controller = "FoxBizService" }
            );

            // CORS를 지원하는 Fox Biz Service 컨트롤러 라우팅
            config.Routes.MapHttpRoute(
                name: "CorsFoxBizService",
                routeTemplate: "api/cors/bizservice/{action}",
                defaults: new { controller = "CorsBizService" }
            );
        }
    }
}

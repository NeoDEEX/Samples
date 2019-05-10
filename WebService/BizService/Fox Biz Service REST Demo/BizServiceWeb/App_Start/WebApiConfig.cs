using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BizServiceWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // Fox Biz Service REST API 라우팅 설정
            config.Routes.MapHttpRoute(
                name: "FoxBizServiceRoute",
                routeTemplate: "api/bizservice/{action}",
                defaults: new { controller = "FoxBizService" }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace RealGridApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Web API 초기화
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // Fox Biz/Data Service 초기화
            TheOne.ServiceModel.Biz.FoxBizServiceConfig.Configure();
            // MVC 초기화
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
    }
}

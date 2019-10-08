using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace DataServiceWeb
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // NeoDEEX REST API 초기화 수행
            TheOne.ServiceModel.Biz.FoxBizServiceConfig.Configure();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}

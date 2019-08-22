using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BizServiceWeb
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // FoxBizService 구성 수행
            TheOne.ServiceModel.Biz.FoxBizServiceConfig.Configure();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}

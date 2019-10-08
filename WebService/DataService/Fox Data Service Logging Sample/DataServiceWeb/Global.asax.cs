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
            // NeoDEEX REST API �ʱ�ȭ ����
            TheOne.ServiceModel.Biz.FoxBizServiceConfig.Configure();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}

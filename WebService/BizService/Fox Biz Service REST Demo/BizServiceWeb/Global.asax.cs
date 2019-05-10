using System;
using System.Web.Http;

namespace BizServiceWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Web API 초기화
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // 비즈 서비스를 구성합니다.
            TheOne.ServiceModel.Biz.FoxBizServiceConfig.Configure();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
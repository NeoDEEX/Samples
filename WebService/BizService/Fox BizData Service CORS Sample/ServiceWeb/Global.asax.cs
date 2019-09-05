using System.Web.Http;
using TheOne.ServiceModel.Biz;

namespace ServiceWeb
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // BIZ 서비스 초기화
            FoxBizServiceConfig.Configure();
            // Web API 초기화
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}

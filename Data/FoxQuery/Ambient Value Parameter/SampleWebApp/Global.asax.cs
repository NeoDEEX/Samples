using ServerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace SampleWebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Fox Biz/Data Service configuration
            TheOne.ServiceModel.Biz.FoxBizServiceConfig.Configure();

            // Register custom ambient value source
            TheOne.Data.Extensions.FoxAmbientValueManager.AddValueSource("Env", new CustomValueSource());

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}

using System.Web.Http.Cors;

namespace ServiceWeb.Controllers
{
    //
    // CORS를 지원하는 BizService 구현
    //
    // NuGet에서 Microsoft.AspNet.WebApi.Cors 패키지를 추가하고 EnableCorsAttribute를 WebApi 컨트롤러에
    // 추가해야 한다. EnableCorsAttribute 특성에 CORS 호출을 허용할 도메인과 호출에 포함될 수 있는 헤더,
    // HTTP 메서드를 명시해 주면 된다. 또한 WebApiConfig.cs에 EnableCors 확장 메서드 호출을 포함해 주면 된다.
    //
    // 관련 자료:
    // https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api
    //
    [EnableCors(origins: "http://localhost:7512,http://localhost.fiddler:7512", headers: "*", methods: "*")]
    public class CorsBizServiceController : TheOne.ServiceModel.Web.Http.Controllers.FoxBizServiceController
    {
    }
}

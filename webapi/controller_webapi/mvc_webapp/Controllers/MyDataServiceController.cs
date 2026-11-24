using Microsoft.AspNetCore.Mvc;
using NeoDEEX.Diagnostics;
using NeoDEEX.Security;
using NeoDEEX.ServiceModel.Data;
using NeoDEEX.ServiceModel.WebApi;
using NeoDEEX.ServiceModel.WebApi.Controllers;
using NeoDEEX.ServiceModel.WebApi.Filters;
using System.Text;

namespace mvc_webapp.Controllers;

[Route("/api/my_service")]
[FoxAuthorize]
[FoxServiceResult(UseTypeInfo = true)]
[FoxServiceExceptionHandler(DetailedErrorInfo = false)]
public class MyDataServiceController : FoxDataServiceControllerBase
{
    public record LoginRequest(string UserName, string Password);

    private readonly IFoxLog Log;

    public MyDataServiceController()
    {
        this.Log = FoxLogManager.GetLogger<MyDataServiceController>();
        this.Log.Verbose("MyDataServiceController is activated.");
    }

    [HttpPost("login")]
    [FoxAuthorize(false)]
    public IActionResult Login([FromBody]LoginRequest userInfo)
    {
        this.Log.VerboseFormat("Login action invoked. UserName: {0}", userInfo.UserName);
        // 로그인 수행
        if (userInfo.UserName == "test_user" && userInfo.Password == "password")
        {
            this.Log.Verbose("User authenticated successfully.");
        }
        else
        {
            this.Log.WarningFormat("Authentication failed for user: {0}", userInfo.UserName);
            throw new UnauthorizedAccessException("Invalid username or password.");
        };
        // 이 예제는 단순한 액세스 토큰을 반환하지만 JWT 토큰과 같이 보안을 고려한 다른 액세스 토큰을 사용해야 한다.
        FoxUserInfoContext ctx = new(userInfo.UserName);
        string accessToken = FoxUserInfoContext.VersionIndependentSerialize(ctx);
        return this.Ok(new { accessToken});
    }

    // HTTP GET 을 사용하는 커스텀 API 예제 코드
    [HttpGet("products/{id}")]
    [FoxServiceResult(UseTypeInfo = false)]
    [FoxAuthorize(false)]
    public FoxDataResponse Proudcts(string id)
    {
        this.Log.VerboseFormat("Proudcts action invoked.");
        FoxDataRequest request = new("products.get");
        request.Parameters.Add("product_id", id);
        return base.ExecuteDataSet(request);
    }

    [HttpPost("upload")]
    [FoxServiceExceptionHandler(DetailedErrorInfo = true)]
    public FoxDataResponse Upload([FoxFromBody] FoxDataRequest request, IFormFile file)
    {
        this.Log.Verbose("Upload action invoked.");

        using Stream stream = file.OpenReadStream();
        StreamReader reader = new(stream, Encoding.UTF8);
        string content = reader.ReadToEnd();
        this.Log.VerboseFormat("File content: {0}", content);
        
        return base.ExecuteDataSet(request);
    }
}

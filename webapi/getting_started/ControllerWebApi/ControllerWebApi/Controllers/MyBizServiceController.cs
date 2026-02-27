using Microsoft.AspNetCore.Mvc;
using NeoDEEX.ServiceModel.Biz;
using NeoDEEX.ServiceModel.WebApi.Controllers;
using NeoDEEX.ServiceModel.WebApi.Filters;

namespace ControllerWebApi.Controllers;

[Route("biz/service")]
[FoxServiceResult(UseTypeInfo = false)]
public class MyBizServiceController : FoxBizServiceControllerBase
{
    [HttpGet("echo")]
    public FoxBizResponse Get_Echo(string msg)
    {
        FoxBizRequest request = new("BizLogicModule.BizLogic", "Echo");
        request["message"] = "---" + msg + "---";
        FoxBizResponse response = base.Execute(request);
        return response;
    }
}

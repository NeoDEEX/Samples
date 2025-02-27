using NeoDEEX.Security;
using NeoDEEX.ServiceModel.Services.Biz;

namespace webapi_app.Biz;

[FoxBizClass]
public class TestBizLogic
{
    [FoxBizMethod]
    public string GetHello()
    {
        FoxUserInfoContext? ctx = FoxUserInfoContext.Current;
        return $"Hello, NeoDEEX FoxBizService World! from {FoxUserInfoContext.Current?.UserId}   {ctx}";
    }
}

using NeoDEEX.ServiceModel.Services.Biz;

namespace webapi_app.Biz;

[FoxBizClass]
public class TestBizLogic
{
    [FoxBizMethod]
    public string GetHello()
    {
        return "Hello, NeoDEEX FoxBizService World!";
    }
}

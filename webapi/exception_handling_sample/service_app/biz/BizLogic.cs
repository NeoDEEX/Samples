using NeoDEEX;
using NeoDEEX.ServiceModel.Services.Biz;

namespace service_app.biz;

[FoxBizClass("BizLogic")]
public class BizLogic
{
    [FoxBizMethod]
    public string Echo(string input)
    {
        return "ECHO: " + input;
    }

    [FoxBizMethod]
    public void ErrorMethod()
    {
        throw new FoxException("Test Error from biz logic", 9999);
    }
}

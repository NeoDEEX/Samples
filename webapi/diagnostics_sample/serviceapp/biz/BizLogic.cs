using NeoDEEX.Diagnostics;
using NeoDEEX.ServiceModel.Services.Biz;

namespace serviceapp.biz;

[FoxBizClass("BizLogic")]
public class BizLogic
{
    [FoxBizMethod]
    public string Echo(string input)
    {
        return "ECHO: " + input;
    }

    [FoxBizMethod]
    public void HelloWorld()
    {
        var ctx = FoxBizServiceContext.Current;
        ctx?.WriteLog(FoxLogLevel.Information, "-- BizMethod Log: Hello Biz Service World !");
    }
}

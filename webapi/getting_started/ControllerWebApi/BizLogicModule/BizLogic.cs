using NeoDEEX.ServiceModel.Services.Biz;

namespace BizLogicModule;

#pragma warning disable CA1822

[FoxBizClass]
public class BizLogic
{
    [FoxBizMethod]
    public string Echo(string message)
    {
        return "ECHO:" + message;
    }
}

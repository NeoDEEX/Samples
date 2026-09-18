using NeoDEEX.ServiceModel.Services.Biz;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace BizSample;

[FoxBizClass]
public class MyBizLogic
{
    [FoxBizMethod]
    public string Echo(string message)
    {
        return "ECHO: " + message;
    }
}

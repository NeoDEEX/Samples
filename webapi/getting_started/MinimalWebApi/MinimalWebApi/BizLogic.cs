using NeoDEEX.ServiceModel.Services.Biz;

namespace MinimalWebApi;

[FoxBizClass]
public class BizLogic
{
    [FoxBizMethod]
    public string Hello()
    {
        return "Hello Fox Biz Service World";
    }
}

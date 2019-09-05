using System;
using TheOne.ServiceModel.Biz;

namespace ServiceWeb.Biz
{
    //
    // 테스트를 위한 Biz 클래스
    //
    [FoxBizClass]
    public class BizClass
    {
        [FoxBizMethod]
        public string Echo(string str)
        {
            return $"[{DateTime.Now}] Echo from biz service: " + str;
        }
    }
}
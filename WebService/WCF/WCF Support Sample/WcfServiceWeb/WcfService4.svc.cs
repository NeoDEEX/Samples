using InterfaceLib;
using System;
using TheOne.Security;
using TheOne.ServiceModel;

namespace WcfServiceWeb
{
    // 
    // 데모를 위한 네번째 WCF 서비스 클래스 구현
    //
    [FoxAuthentication(AllowAnonymous = false)]
    public class WcfService4 : IWcfService4
    {
        public string EchoUserInfo()
        {
            // 호출한 사용자를 나타내는 UserInfoContext 객체 획득
            var ctx = FoxUserInfoContext.Current;
            if (ctx == null)
            {
                throw new UnauthorizedAccessException("인증되지 않은 사용자");
            }
            return $"UserId:{ctx.UserId},  DeptId={ctx["DeptId"]}";
        }

        public void Dispose()
        {
            // 구현 내용이 없다.
        }
    }
}

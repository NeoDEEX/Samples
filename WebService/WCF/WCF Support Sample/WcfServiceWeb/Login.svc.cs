using InterfaceLib;
using System;
using TheOne.Security;
using TheOne.ServiceModel;

namespace WcfServiceWeb
{
    // 
    // 데모를 위한 네번째 WCF 서비스 클래스 구현
    //
    [FoxAuthentication(AllowAnonymous = true)]
    public class Login : ILogin
    {
        public bool LoginUser(string userId, string password)
        {
            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(password))
            {
                return false;
            }
            if (userId.Equals("tester", StringComparison.OrdinalIgnoreCase) == false)
            {
                return false;
            }
            if (password.Equals("passport", StringComparison.OrdinalIgnoreCase) == false)
            {
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            // 구현 내용이 없다.
        }
    }
}

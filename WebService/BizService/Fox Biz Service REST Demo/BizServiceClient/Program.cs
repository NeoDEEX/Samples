using System;
using TheOne.ServiceModel.Biz;
using TheOne.ServiceModel.Web;

namespace BizServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Request 객체 구성
            var request = new FoxBizRequest("데모비즈", "Echo");
            request.Parameters["input"] = "Wow!";

            // 비즈 서비스(WCF) 호출
            using (var client = new FoxBizClient("BizService.svc"))
            {
                var response = client.Execute(request);
                Console.WriteLine($"Result: {response.Result}");
            }

            // 비즈 서비스 REST API 호출
            using (var client = new FoxRestBizClient("api/bizservice"))
            {
                var response = client.Execute(request);
                Console.WriteLine($"Result: {response.Result}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOne.ServiceModel.Biz;

namespace BizServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new FoxBizClient("BizService.svc"))
            {
                var request = new FoxBizRequest("데모비즈", "Echo");
                // 매개변수를 설정 합니다.
                request.Parameters["input"] = "Wow!";

                var response = client.Execute(request);
                Console.WriteLine($"Result: {response.Result}");
            }
        }
    }
}

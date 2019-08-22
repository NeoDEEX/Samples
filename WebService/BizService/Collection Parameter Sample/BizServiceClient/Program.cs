using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOne.ServiceModel;
using TheOne.ServiceModel.Biz;
using TheOne.ServiceModel.Web;

namespace BizServiceClient
{
    class Program
    {
        static string restUrl = "api/bizservice";
        static string wcfUrl = "bizservice.svc";

        static void Main(string[] args)
        {
            // WCF 서비스 호출
            InvokeSimpleTestMethod(1, new FoxBizClient(wcfUrl), "WCF-Test1");
            InvokeSimpleTestMethod(2, new FoxBizClient(wcfUrl), "WCF-Test2");
            InvokeSimpleTestMethod(3, new FoxBizClient(wcfUrl), "WCF-Test3");
            InvokeComplextTestMethod1(new FoxBizClient(wcfUrl), "WCF-Test4");
            // REST API 서비스 호출
            InvokeSimpleTestMethod(1, new FoxRestBizClient(restUrl), "REST-Test1");
            InvokeSimpleTestMethod(2, new FoxRestBizClient(restUrl), "REST-Test2");
            InvokeSimpleTestMethod(3, new FoxRestBizClient(restUrl), "REST-Test3");
            InvokeComplextTestMethod1(new FoxRestBizClient(restUrl), "REST-Test4");
        }

        // 단순 컬렉션(1개의 IDictionary)을 매개변수로 취하는 비즈 메서드 호출
        static void InvokeSimpleTestMethod(int index, IFoxBizClient client, string prefix)
        {
            Console.WriteLine($"--- Execute SimpleTestMethod{index} :");
            var request = new FoxBizRequest("BizServiceWeb.Biz.TestBizClass", $"SimpleTestMethod{index}", GenerateParameter(prefix));
            using (client)
            {
                var response = client.Execute(request);
                Console.WriteLine(response.Result);
            }
        }

        // 여러 IDictionary 객체를 매개변수로 취하는 비즈 메서드 호출
        static void InvokeComplextTestMethod1(IFoxBizClient client, string prefix)
        {
            Console.WriteLine($"--- Execute ComplexTestMethod1 :");
            var request = new FoxBizRequest("BizServiceWeb.Biz.TestBizClass", $"ComplexTestMethod1");
            request["dic1"] = GenerateParameter(prefix);
            request["dic2"] = GenerateParameter(prefix);
            using (client)
            {
                var response = client.Execute(request);
                Console.WriteLine(response.Result);
            }
        }

        // 테스트를 위한 매개변수를 생성한다.
        static IDictionary<string, object> GenerateParameter(string prefix)
        {
            return new Dictionary<string, object>()
            {
                { $"{prefix}-param1", "string1" },
                { $"{prefix}-param2", 2 },
                { $"{prefix}-param3", 3.3 },
            };
        }
    }
}

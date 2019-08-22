using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TheOne.ServiceModel;
using TheOne.ServiceModel.Biz;

namespace BizServiceWeb.Biz
{
    [FoxBizClass]
    public class TestBizClass
    {
        // FoxBizRequest 객체를 매개변수로 반환하는 메서드
        [FoxBizMethod]
        public string SimpleTestMethod1(FoxBizRequest request)
        {
            return DumpParameter(request.Parameters);
        }

        // FoxServiceParameterCollection 객체를 매개변수로 반환하는 메서드
        [FoxBizMethod]
        public string SimpleTestMethod2(FoxServiceParameterCollection parameters)
        {
            return DumpParameter(parameters);
        }

        // IDictionary<string, object> 객체를 매개변수로 반환하는 메서드
        [FoxBizMethod]
        public string SimpleTestMethod3(IDictionary<string, object> dic)
        {
            return DumpParameter(dic);
        }

        // IDictionary<string, object> 객체를 여러 개 매개변수로 반환하는 메서드
        // 주) 4.5.4 버전 이상에서만 호출 가능. 이전 버전에서는 직렬화 오류 발생.
        [FoxBizMethod]
        public string ComplexTestMethod1(IDictionary<string, object> dic1, IDictionary<string, object> dic2)
        {
            return DumpParameter(dic1) + DumpParameter(dic2);
        }

        // 매개변수를 문자열 표현으로 바꾸어 반환한다.
        private string DumpParameter(IDictionary<string, object> parameters)
        {
            var sb = new StringBuilder();
            foreach (var parameter in parameters)
            {
                sb.Append($"[{parameter.Key}] = {parameter.Value}").AppendLine();
            }
            return sb.ToString();
        }

    }
}
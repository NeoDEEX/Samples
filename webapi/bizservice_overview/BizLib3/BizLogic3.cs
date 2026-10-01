using NeoDEEX.Diagnostics;
using NeoDEEX.ServiceModel;
using NeoDEEX.ServiceModel.Biz;
using NeoDEEX.ServiceModel.Services.Biz;
using System.Text;

namespace BizLib3;

#pragma warning disable CA1822

[FoxBizClass]
public class BizLogic3
{
    private FoxBizServiceContext? _ctx;

    public BizLogic3()
    {
        _ctx = FoxBizServiceContext.Current;
    }

    [FoxBizMethod]
    public string ParamBinding1(string param1, decimal param2)
    {
        return $"Echo parameters: param1={param1}, param2={param2}";
    }

    [FoxBizMethod]
    public string ParamBinding2(IDictionary<string, object?> parameters)
    {
        //string param1 = (string)parameters["param1"]!;
        //decimal param2 = (decimal)parameters["param2"]!;
        //return $"Echo parameters: param1={param1}, param2={param2}";
        return DumpParameters(parameters);
    }

    [FoxBizMethod]
    public string ParamBinding3(FoxBizRequest request)
    {
        FoxClientInfo? clientInfo = request.ClientInfo;
        WriteLog("Request from {0}", clientInfo?.IP);
        return DumpParameters(request.Parameters);
    }

    private static void WriteLog(string format, params object?[] args)
    {
        FoxBizServiceContext? ctx = FoxBizServiceContext.Current;
        ctx?.WriteLog(FoxLogLevel.Verbose, format, args);
    }

    static string DumpParameters(IDictionary<string, object?> parameters)
    {
        StringBuilder sb = new();
        sb.Append("Echo parameters: ");
        foreach (string key in parameters.Keys)
        {
            sb.Append(key).Append('=');
            if (parameters[key] != null)
            {
                sb.Append(parameters[key]);
            }
            else
            {
                sb.Append("(null)");
            }
            sb.Append(", ");
        }
        sb.Remove(sb.Length - 2, 2);
        return sb.ToString();
    }

    [FoxBizMethod]
    public FoxBizResponse ReturnBinding(out string outputParameter)
    {
        outputParameter = "output value";
        FoxBizResponse response = new()
        {
            Result = "ReturnBinding Sample"
        };
        response.Parameters["out1"] = "output #1";
        response.Parameters["out2"] = 99.99;
        return response;
    }

    [FoxBizMethod]
    public string OutputParamTest(string input, out string output)
    {
        output = $"Output ECHO: {input}";
        return $"Return ECHO: {input}";
    }
}

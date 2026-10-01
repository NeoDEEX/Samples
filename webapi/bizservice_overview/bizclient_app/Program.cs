using NeoDEEX.ServiceModel.Biz;
using NeoDEEX.ServiceModel.Client.Biz;
using Spectre.Console;
using System.Data;
using System.Text.Json;

namespace bizclient_app;

internal class Program
{
    static async Task Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Biz Service BasicSample Client...[/]");
        AnsiConsole.WriteLine();

        await SimpleInvoke();
        await SearchProduct();
        await SearchProduct_Untyped();
        await ParameterBindingTest();
        await OutputParameterTest();
    }

    static async Task SimpleInvoke()
    {
        FoxBizRequest request = new("BizSample.MyBizLogic", "Echo");
        request.Parameters["message"] = "Hello, Fox Biz Service World!";
        FoxBizServiceClient client = new("api/bizservice");
        FoxBizResponse response = await client.ExecuteAsync(request);
        AnsiConsole.MarkupLine($"[gray]Biz Service Response:[/] [yellow]{response.Result}[/]");
    }

    // 검색 메서드 호출
    // 주) DataSet 반환 타입은 타입 정보를 포함할 때(useTypeInfo=true)만 자동으로 역직렬화 된다.
    static async Task SearchProduct()
    {
        FoxBizRequest request = new("BizLib2.BizLogic2", "SearchProduct");
        request.Parameters["name"] = "Chef";
        FoxBizServiceClient client = new("api/typed/bizservice");
        FoxBizResponse response = await client.ExecuteAsync(request);
        DataSet ds = (DataSet)response.Result;
        AnsiConsole.MarkupLine($"[yellow]Result Product Names:[/]");
        foreach(DataRow row in ds.Tables[0].Rows)
        {
            AnsiConsole.MarkupLine($"    [gray]{row["product_name"]}[/]");
        }
    }

    // JsonSerializerOptions 객체는 매번 생성하면 성능이 저하되므로 한번 생성 후 캐시해야 한다.
    static Lazy<JsonSerializerOptions> _Options = new(() =>
    {
        JsonSerializerOptions options = new(NeoDEEX.Text.Json.FoxJsonUtils.DefaultJsonSerializerOptions);
        options.Converters.Add(new NeoDEEX.Text.Json.FoxDataSetConverter());
        return options;
    });

    // useTypeInfo = false 일 때는 DataSet 타입은 자동으로 역직렬화되지 않는다.
    // 따라서 Result 속성이 반환하는 JsonElement 객체를 직접 역직렬화 해야 한다.
    // 직접 역직렬화를 위해서는 FoxDataSetConverter 를 사용하는 JsonSerializerOptions 객체가
    // 필요하다.
    static async Task SearchProduct_Untyped()
    {
        FoxBizRequest request = new("BizLib2.BizLogic2", "SearchProduct");
        request.Parameters["name"] = "Chef";
        FoxBizServiceClient client = new("api/bizservice");
        FoxBizResponse response = await client.ExecuteAsync(request);
        JsonElement element = (JsonElement)response.Result;
        DataSet ds = element.Deserialize<DataSet>(_Options.Value)!;
        AnsiConsole.MarkupLine($"[yellow]Result Product Names:[/]");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            AnsiConsole.MarkupLine($"    [gray]{row["product_name"]}[/]");
        }
    }

    static async Task ParameterBindingTest()
    {
        await ParamBindingTestAsync("ParamBinding1");
        await ParamBindingTestAsync("ParamBinding2");
        await ParamBindingTestAsync("ParamBinding3");
        await ReturnBindingTestAsync();

        static async Task ParamBindingTestAsync(string methodId)
        {
            FoxBizRequest request = new("BizLib3.BizLogic3", methodId);
            request.Parameters["param1"] = "StringValue1";
            request.Parameters["param2"] = 2.22M;
            FoxBizServiceClient client = new("api/bizservice");
            FoxBizResponse response = await client.ExecuteAsync(request);
            AnsiConsole.MarkupLine($"[yellow]{methodId} Result:[/] [gray]{response.Result}[/]");
        }

        static async Task ReturnBindingTestAsync()
        {
            FoxBizRequest request = new("BizLib3.BizLogic3", "ReturnBinding");
            FoxBizServiceClient client = new("api/bizservice");
            FoxBizResponse response = await client.ExecuteAsync(request);
            AnsiConsole.MarkupLine($"[yellow]ReturnBinding Result:[/] [gray]{response.Result}[/]");
            // FoxBizResponse 객체를 반환하는 BizLogic3.ReturnBinding 메서드의 경우, Fox Biz Service 는 자동으로
            // output 매개변수를 처리하지 않는다. 따라서 FoxBizResponse 객체의 Parameters 컬렉션에는 outputParameter 키는 존재하지 않는다.
            AnsiConsole.MarkupLine($"    [gray]outputParam key exists: {response.Parameters.ContainsKey("outputParameter")}[/]");
            AnsiConsole.MarkupLine($"    [gray]output parameter: out1={response["out1"]}, out2={response["out2"]}[/]");
        }
    }

    static async Task OutputParameterTest()
    {
        FoxBizRequest request = new("BizLib3.BizLogic3", "OutputParamTest");
        request.Parameters["input"] = "Input_String";
        FoxBizServiceClient client = new("api/bizservice");
        FoxBizResponse response = await client.ExecuteAsync(request);
        AnsiConsole.MarkupLine($"[yellow]Return value:[/] [gray]{response.Result}[/]");
        AnsiConsole.MarkupLine($"    [gray]output parameter: output={response["output"]}[/]");
    }
}

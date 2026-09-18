using NeoDEEX.ServiceModel.Biz;
using NeoDEEX.ServiceModel.Client.Biz;
using Spectre.Console;

namespace bizclient_app;

internal class Program
{
    static async Task Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Biz Service BasicSample Client...[/]");
        AnsiConsole.WriteLine();

        await SimpleInvoke();
    }

    static async Task SimpleInvoke()
    {
        FoxBizRequest request = new("BizSample.MyBizLogic", "Echo");
        request.Parameters["message"] = "Hello, Fox Biz Service World!";
        FoxBizServiceClient client = new("api/bizservice");
        FoxBizResponse response = await client.ExecuteAsync(request);
        AnsiConsole.MarkupLine($"[gray]Biz Service Response:[/] [yellow]{response.Result}[/]");
    }
}

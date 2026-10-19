using NeoDEEX.ServiceModel.Client.Data;
using NeoDEEX.ServiceModel.Data;
using Spectre.Console;

namespace client_app;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Biz/Data Service Exception Handling Sample Client...[/]");

        ResetProductsTable();

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[cyan]> Data Service Test ========================================[/]");
        DataServiceTest.DoTest();
        AnsiConsole.MarkupLine("[cyan]> Biz Service Test =========================================[/]");
        BizServiceTest.DoTest();
    }

    static void ResetProductsTable()
    {
        AnsiConsole.MarkupLine("[blue]Reset Products Table...[/]");
        FoxDataServiceClient client = new("/api/dataservice");
        FoxDataRequest request = new("sample.reset_test") { Operation = FoxDataOperations.ExecuteNonQuery };
        client.ExecuteNonQuery(request);
    }

}

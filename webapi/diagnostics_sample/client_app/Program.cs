using Spectre.Console;

namespace client_app;

internal static class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Biz/Data Service Diagnostics Sample Client...[/]");
        AnsiConsole.WriteLine();

        DataServiceTest.DoTest();
        BizServiceTest.DoTest();
    }
}

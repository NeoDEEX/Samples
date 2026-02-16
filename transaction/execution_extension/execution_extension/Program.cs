using Spectre.Console;

namespace execution_extension;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Transaction Execution Extension Sample...[/]\n");

        using BizClass biz = new();
        IBizClass itf = biz.CreateExecution<IBizClass>();
        try
        {
            itf.Method1("Invocation #1");
            itf.Method1("Invocation #2");
            itf.Method2();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
        }
    }
}

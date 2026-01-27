using common;
using Spectre.Console;

namespace fox_transactions_local_tx;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Transactions Local Transaction Test[/]");

        TestUtils.TestDB1 = "Oracle";
        TestUtils.TestDB2 = null;
        TestUtils.SetupTest<Program>();

        TestUtils.DumpTables("Before Local Transaction: ");

        bool commit = false;
        using BizClass biz = new();
        IBizClass itf = biz.CreateExecution<IBizClass>();
        try
        {
            itf.InsertMany([997, 998, 999], commit);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]EXCEPTION: {ex.Message}[/]");
        }

        TestUtils.DumpTables($"After Local Transaction: ", commit ? "Commit" : "Rollback");
    }
}

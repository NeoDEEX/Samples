using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using NeoDEEX.Transactions;
using NeoDEEX.Transactions.Common;
using Spectre.Console;

namespace execution_context;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Transactions Execution Context Sample[/]");
        SetupTest();
        AnsiConsole.WriteLine();

        FoxExecutionContext.Current.Dump("In base code (main method) ...");

        IBizClass biz = new BizClass().CreateExecution<IBizClass>();
        try
        {
            biz.InsertMany([997, 998, 999], true);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        }
        //biz.ShowContextInfo();
    }

    static void SetupTest()
    {
        // user-secrets에서 DB 연결정보를 로드하도록 구성 설정 지정.
        FoxDatabaseConfig.ExternalConfiguration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

        AnsiConsole.Markup("[gray]Test databases have been set up...[/]");

        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess("PostgreSQL");
        dbAccess.Open();
        dbAccess.ExecuteSqlNonQuery("DELETE FROM TxTestTable");
        dbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(1, 1, 'A')");
        dbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(2, 2, 'B')");
        dbAccess.Close();

        AnsiConsole.MarkupLine("[darkgreen] Done.[/]");
    }
}

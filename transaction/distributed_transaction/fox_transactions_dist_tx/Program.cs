using common;
using Spectre.Console;
using System.Transactions;

namespace fox_transactions_dist_tx;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Transactions Distributed Transaction Test[/]");
        // 분산 트랜잭션 프로모션이 작동하기 위해 필요한 설정
        // 주) Npgsql 은 이 설정이 없어도 프로모션이 작동하지만 Oralce, SQL Server 등은 이 설정이 필요하다.
        TransactionManager.ImplicitDistributedTransactions = true;

        TestUtils.TestDB1 = "Oracle";
        TestUtils.TestDB2 = "SqlServer";
        TestUtils.SetupTest<Program>();

        TestUtils.DumpTables("Before Distributed Transaction: ");

        bool commit = true;
        using BizClass biz = new();
        IBizClass itf = biz.CreateExecution<IBizClass>();
        try
        {
            itf.DoDistributedTransaction(commit);
            //itf.InsertMany([997, 998, 999], commit);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]EXCEPTION: {ex.Message}[/]");
        }

        TestUtils.DumpTables($"After Distributed Transaction: ", commit ? "Commit" : "Rollback");
    }
}

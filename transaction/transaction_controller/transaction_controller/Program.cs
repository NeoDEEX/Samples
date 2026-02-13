using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using Spectre.Console;
using System.Transactions;

namespace transaction_controller;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Transactions - Transaction Controller Sample[/]\n");
        SetupTest();

        using BizClass biz = new();
        IBizClass proxy = biz.CreateExecution<IBizClass>();
        proxy.Implicit();
        proxy.Default();
        proxy.RootContext();

        bool forceRollback = true;
        try
        {
            AnsiConsole.WriteLine();
            proxy.InsertMany_DistTx([94, 95, 96], forceRollback);
            //proxy.InsertMany_CustomTx([94, 95, 96], forceRollback);
            DumpMemo("After invoke InsertMany():");
        }
        catch (ApplicationException ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            DumpMemo("After ROLLBACK:");
        }

        try
        {
            AnsiConsole.WriteLine();
            proxy.InsertMany_LocalTx([97, 98, 99], forceRollback);
            DumpMemo("After invoke InsertMany():");
        }
        catch (ApplicationException ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            DumpMemo("After ROLLBACK:");
        }

        try
        {
            AnsiConsole.WriteLine();
            proxy.DoBadDbAccess();
            DumpMemo("After invoke InsertMany():");
        }
        catch (ApplicationException ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            DumpMemo("After ROLLBACK:");
        }
    }

    static void SetupTest()
    {
        // user-secrets에서 DB 연결정보를 로드하도록 구성 설정 지정.
        FoxDatabaseConfig.ExternalConfiguration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

        AnsiConsole.Markup("[gray]Test databases have been set up...[/]");

        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        dbAccess.Open();
        dbAccess.ExecuteSqlNonQuery("DELETE FROM memo_data");
        dbAccess.ExecuteSqlNonQuery("INSERT INTO memo_data (title, content) VALUES ('Memo #1', '이것은 첫 번째 메모의 내용입니다.')");
        dbAccess.ExecuteSqlNonQuery("INSERT INTO memo_data (title, content) VALUES ('Memo #2', '이것은 두 번째 메모의 내용입니다.')");
        dbAccess.Close();

        // FastTransaction 컨트롤러 사용 시 분산 트랜잭션 메시지가 출력되지 않는다면
        // 연결 풀링 덕분일 수 있으므로 연결 문자열에 pooling=false 옵션을 추가하면
        // 분산 트랜잭션이 활성화 되는 것을 확인할 수 있다.
        TransactionManager.ImplicitDistributedTransactions = true;
        TransactionManager.DistributedTransactionStarted += (sender, e) =>
        {
            AnsiConsole.MarkupLine($"[blue]Distributed Transaction Started: ID =[/][blue]{e.Transaction?.TransactionInformation.DistributedIdentifier}[/]");
        };

        AnsiConsole.MarkupLine("[darkgreen] Done.[/]\n");
    }

    static void DumpMemo(string header)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        List<Memo> memos = dbAccess.ExecuteQueryList<Memo>("memo.getall");
        DumpMemo(header, memos);
    }

    static void DumpMemo(string header, List<Memo> memos)
    {
        AnsiConsole.MarkupLine($"[yellow]{Markup.Escape(header)}[/]");
        foreach (Memo memo in memos)
        {
            AnsiConsole.MarkupLine($"  [gray]{memo.Id,-8}{memo.Title,-16}{memo.Content}[/]");
        }
    }
}

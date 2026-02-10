using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using Spectre.Console;
using System.Data;

namespace dac_base_demo;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Transactions FoxDacBase Class Sample[/]\n");
        SetupTest();

        Memo[] memos = [
            new Memo { Title = "Memo A", Content = "Content of Memo A" },
            new Memo { Title = "Memo B", Content = "Content of Memo B" },
            new Memo { Title = "Memo C", Content = "Content of Memo C" }
        ];
        using BizClass biz = new();
        IBizClass itf = biz.CreateExecution<IBizClass>();
        try
        {
            // 로컬 트랜잭션 컨트롤러 롤백 테스트
            bool rollback = true;
            itf.InsertMany(memos, rollback);
            DumpMemo("After invoke InsertMany():");

            // 로컬 트랜잭션 컨트롤러 사용시 Open/Close 메서드 호출 시 발생하는 예외 테스트
            //List<Memo> resultMemos = itf.Insert(memos[0]);
            //DumpMemo("After invoke Insert():", resultMemos);
        }
        catch (Exception ex)
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
        AnsiConsole.MarkupLine($"[white]{Markup.Escape(header)}[/]");
        foreach (Memo memo in memos)
        {
            AnsiConsole.MarkupLine($"  [gray]{memo.Id,-8}{memo.Title,-16}{memo.Content}[/]");
        }
    }
}

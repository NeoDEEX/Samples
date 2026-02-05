using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using NeoDEEX.Transactions.Common;
using Spectre.Console;
using System.Data;

namespace execution_context;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Transactions Execution Context Sample[/]");
        SetupTest();
        AnsiConsole.WriteLine();

        FoxExecutionContext.Current.Dump("In base code (main method) ...");

        bool commit = false;
        BizClass biz = new();
        // 수행 프록시 생성
        IBizClass proxy = biz.CreateExecution<IBizClass>();
        // 수행 프록시를 통해 호출하면 예외에 따라 트랜잭션이 롤백/커밋됨.
        AnsiConsole.MarkupLine("\n[yellow]-- Test 1: Fox Transactions case --[/]");
        DoTest(proxy, commit);
        // 직접 호출(프록시 아님)은 트랜잭션을 사용하지 않으므로 예외가 발생해도 롤백되지 않음.
        AnsiConsole.MarkupLine("\n[yellow]-- Test 2: Non-Transactional case --[/]");
        DoTest(biz, commit);

        static void DoTest(IBizClass biz, bool commit)
        {
            try
            {
                // 수행 프록시를 통해 메서드 호출. 트랜잭션이 시작되고 커밋/롤백이 예외에 의해 결정됨.
                biz.InsertMany([100, 101, 102], commit);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
            //biz.ShowContextInfo();
            ShowTableContents();
        }
    }

    // 테스트용 DB 설정 및 초기 데이터 삽입
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

    // 테스트용 테이블 내용 출력
    static void ShowTableContents()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess("PostgreSQL");
        DataSet ds = dbAccess.ExecuteSqlDataSet("SELECT PK, COL1, COL2 FROM TxTestTable ORDER BY PK");
        AnsiConsole.MarkupLine("[gray]Current contents of TxTestTable:[/]");
        foreach(DataRow row in ds.Tables[0].Rows)
        {
            int pk = (int)row["PK"];
            int col1 = (int)row["COL1"];
            string col2 = (string)row["COL2"];
            if (pk >= 100)
            {
                // 트랜잭션에서 커밋된 데이터는 파란색으로 표시
                AnsiConsole.MarkupLine($"  [blue]PK={pk}, COL1={col1}, COL2={col2}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"  [gray]PK={pk}, COL1={col1}, COL2={col2}[/]");
            }
        }
    }
}

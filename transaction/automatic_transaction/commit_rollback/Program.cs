using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using Spectre.Console;
using System.Data;

namespace commit_rollback;

internal class Program
{
    static void Main()
    {
        // user-secrets에서 DB 연결정보를 로드하도록 구성 설정 지정.
        FoxDatabaseConfig.ExternalConfiguration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

        AnsiConsole.MarkupLine("[green]Fox Transaction Commit/Rollback Sample[/]");
        AnsiConsole.WriteLine();

        int test_id = 10;
        
        // 트랜잭션 객체 생성 및 수행 문맥 생성
        using BizClass biz = new();
        IBizClass itf = biz.CreateExecution<IBizClass>();

        // 디폴트 트랜잭션 투표값 출력(MyTransactionVote 속성)
        itf.DefaultTransactionStatus();
        AnsiConsole.WriteLine();

        // 기존 테스트 데이터 삭제 및 테스트 데이터 출력
        DeleteTestData(test_id);
        AnsiConsole.MarkupLine("[gray]Data before test...[/]");
        DumpTestData(test_id);
        // 커밋을 수행하는 메서드 호출
        //itf.AutcoCompleteMethod(test_id);
        itf.ManualCompleteMethod(test_id);
        // 커밋 확인을 위해 테스트 데이터 출력
        AnsiConsole.MarkupLine("[gray]Data after commit...[/]");
        DumpTestData(test_id);
        AnsiConsole.WriteLine();

        // 기존 테스트 데이터 삭제 및 테스트 데이터 출력
        DeleteTestData(test_id);
        AnsiConsole.MarkupLine("[gray]Data before test...[/]");
        DumpTestData(test_id);
        try
        {
            // 롤백을 수행하는 메서드 호출
            //itf.AutcoCompleteMethod(test_id, true);
            itf.ManualCompleteMethod(test_id, true);
        }
        catch(InvalidOperationException ex)
        {
            // 데이터 출력을 위해 예외를 무시한다. 
            AnsiConsole.MarkupLine($"[red]Exception from biz method= [[{ex.GetType().Name}]]:{ex.Message}[/]");
        }
        // 롤백 확인을 위해 테스트 데이터 출력
        AnsiConsole.MarkupLine("[gray]Data after rollback...[/]");
        DumpTestData(test_id);
    }

    // 이전 테스트에 추가된 데이터들을 삭제
    public static void DeleteTestData(int test_id)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        parameters.AddWithValue("col_id", test_id);
        dbAccess.ExecuteSqlNonQuery("DELETE FROM my_demo_table WHERE col_id >= @col_id", parameters);
    }

    // 테스트 데이터를 출력
    public static void DumpTestData(int test_id)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        DataSet ds = dbAccess.ExecuteSqlDataSet("SELECT col_id, col_str FROM my_demo_table");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            int id = (int)row["col_id"];
            string name = (string)row["col_str"];
            if (id < test_id)
            {
                AnsiConsole.MarkupLine($"    id={id}, name={name}");
            }
            else
            {
                AnsiConsole.MarkupLine($"    [blue]id={id}, name={name}[/]");
            }
        }
    }
}

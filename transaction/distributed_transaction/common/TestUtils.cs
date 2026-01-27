using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using Spectre.Console;
using System.Data;
using System.Text;
using System.Transactions;

namespace common;

public static class TestUtils
{
    public static string? TestDB1 { get; set; } = "Oracle";
    public static string? TestDB2 { get; set; } = "SqlServer";

    public static int NewDataPK { get; set; } = 999;

    public static void SetupTest<TProgram>() where TProgram : class
    {
        // user-secrets에서 DB 연결정보를 로드하도록 구성 설정 지정.
        var assembly = typeof(TProgram).Assembly;
        FoxDatabaseConfig.ExternalConfiguration = new ConfigurationBuilder().AddUserSecrets(assembly).Build();

        AnsiConsole.Markup("[gray]Test databases have been set up...[/]");

        if (String.IsNullOrEmpty(TestDB1) == false)
        {
            using FoxDbAccess dbAccess1 = FoxDbAccess.CreateDbAccess(TestDB1);
            dbAccess1.DeleteTestData();
        }
        if (String.IsNullOrEmpty(TestDB2) == false)
        {
            using FoxDbAccess dbAccess2 = FoxDbAccess.CreateDbAccess(TestDB2);
            dbAccess2.DeleteTestData();
        }
        ShowTransactionPromotion();

        AnsiConsole.MarkupLine("[darkgreen] Done.[/]");
    }

    public static void ShowTransactionPromotion()
    {
        TransactionManager.DistributedTransactionStarted += (s, e) =>
        {
            AnsiConsole.MarkupLine("[cyan]==> Distributed Transaction Started.[/]");
        };
    }

    public static void DeleteTestData(this FoxDbAccess dbAccess)
    {
        dbAccess.Open();
        dbAccess.ExecuteSqlNonQuery("DELETE FROM TxTestTable");
        dbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(1, 1, 'A')");
        dbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(2, 2, 'B')");
        dbAccess.Close();
    }

    public static void DumpTables(string message, string? coloredMessage = null)
    {
        AnsiConsole.Markup($"[gray]{message}[/]");
        if (coloredMessage != null)
        {
            AnsiConsole.Markup($"[yellow]{coloredMessage}[/]");
        }
        AnsiConsole.WriteLine();
        if (String.IsNullOrEmpty(TestDB1) == false)
        {
            DumpTable(TestDB1);
        }
        if (String.IsNullOrEmpty(TestDB2) == false)
        {
            DumpTable(TestDB2);
        }
        AnsiConsole.WriteLine();
    }

    // GPT-5 mini 가 제안한 코드를 일부 수정함.
    private static void DumpTable(string connName)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(connName);
        DataTable tableData = dbAccess.ExecuteSqlDataSet("SELECT * FROM TxTestTable").Tables[0];

        // 헤더
        AnsiConsole.Markup($"  [gray]{connName,-14}[/]");

        // 변경: 전체 오른쪽 인덴트(칸 수)
        int tableIndent = 16;
        string indent = new(' ', tableIndent);

        // 컬럼 너비 계산 (헤더 이름도 고려)
        int pkWidth = 4;
        int col1Width = 4;
        int col2Width = 8;

        // 빌드 및 출력
        var sb = new StringBuilder(512);
        // 컬럼 제목 라인
        sb.Append("PK".PadLeft(pkWidth)).Append(" | ");
        sb.Append("COL1".PadLeft(col1Width)).Append(" | ");
        sb.Append("COL2".PadLeft(col2Width)).Append("   ");
        sb.AppendLine();
        sb.Append(indent);
        sb.Append('-', pkWidth).Append("-+-");
        sb.Append(new string('-', col1Width)).Append("-+-");
        sb.Append(new string('-', col2Width)).Append("-  ");
        sb.AppendLine();

        // 데이터 행
        foreach (DataRow row in tableData.Rows)
        {
            int pk = Convert.ToInt32(row["PK"]);
            string color = pk >= 10 ? "blue" : "white";

            string pkText = (row["PK"]?.ToString() ?? string.Empty).PadLeft(pkWidth);
            string col1Text = (row["COL1"]?.ToString() ?? string.Empty).PadLeft(col1Width);
            string col2Text = (row["COL2"]?.ToString() ?? string.Empty).PadLeft(col2Width);

            sb.Append(indent);
            sb.Append($"[{color}]{pkText}[/]").Append(" | ");
            sb.Append($"[{color}]{col1Text}[/]").Append(" | ");
            sb.Append($"[{color}]{col2Text}[/]").Append("   ");
            sb.AppendLine();
        }

        AnsiConsole.Markup(sb.ToString());
    }
}

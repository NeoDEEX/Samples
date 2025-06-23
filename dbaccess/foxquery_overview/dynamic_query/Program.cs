using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using System.Data;

namespace dynamic_query;

internal class Program
{
    static void Main()
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ExternalConfiguration = config;

        //SimpleDynamicQuery();
        //EchoArgs();
        DumpParams();
        //Dynamic_Insert_Manully();
        //Dynamic_IN_Unsafe();
        //Dynamic_IN_Safe();
        //Dynamic_Update_Unsafe();
        //Dynamic_Update_Safe();
        //Dynamic_Insert_Unsafe();
        //Dynamic_Insert_Safe();
    }

    static void SimpleDynamicQuery()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        //var parameters = new { product_name = "%Queso%" };
        var parameters = new { product_name = (string?)null };
        DataSet ds = dbAccess.ExecuteQueryDataSet("sample3.dynamic", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }

    static void EchoArgs()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new Dictionary<string, object>
        {
            { "param1", "Value1" },
            { "param2", "222222" },
            { "param3", "Value3" },
        };
        string? result = dbAccess.ExecuteQueryScalar("sample3.echo_arg", parameters) as string;
        Console.WriteLine($"Result: {result}");
    }

    static void DumpParams()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new Dictionary<string, object>
        {
            { "param1", "Value1" },
            { "param2", DateTime.Now },
            { "param3", "Value3" },
        };
        string? result = dbAccess.ExecuteQueryScalar("sample3.dump_params", parameters) as string;
        Console.WriteLine($"Result: {result}");
    }

    static void DumpDemoTable(FoxDbAccess dbAccess, string label)
    {
        DataSet ds = dbAccess.ExecuteQueryDataSet("sample3.get_demo_table");
        DataTable dt = ds.Tables[0];
        Console.WriteLine($">>> {label} data:");
        Console.WriteLine($"{"col_id",12}|{"col_str",12}|{"col_int",12}");
        Console.WriteLine($"{"------------",12}+{"------------",12}+{"------------",12}");
        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"{row["col_id"],12}|{row["col_str"],12}|{row["col_int"],12}");
        }
        Console.WriteLine();
    }

    static void Dynamic_Insert_Manully()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new Dictionary<string, object>
        {
            { "col_id", 9999 },
            { "col_str", "value_9999" },
            { "col_int", 199991 },
        };
        // 반복 수행을 위해 기존 데이터 삭제
        dbAccess.ExecuteSqlNonQuery("DELETE FROM my_demo_table WHERE col_id = 9999");
        var cmd = dbAccess.CreateCommand("sample3.dynamic_insert_manually", parameters);
        Console.WriteLine($"COMMAND TEXT: {cmd.CommandText}");
        dbAccess.ExecuteCommandNonQuery(cmd);
        //dbAccess.ExecuteQueryNonQuery("sample3.dynamic_insert_manually", parameters);
        DumpDemoTable(dbAccess, "After Insert");
    }

    static readonly int[] categoriesArray = [1, 2];

    // IN 절 생성 예제 코드 (SQL Injectino 위험이 있음)
    static void Dynamic_IN_Unsafe()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new { categories = categoriesArray };
        var cmd = dbAccess.CreateCommand("sample3.dynamic_in", parameters);
        Console.WriteLine($"COMMAND TEXT: {cmd.CommandText}");
        DataSet ds = dbAccess.ExecuteCommandDataSet(cmd);
        //DataSet ds = dbAccess.ExecuteQueryDataSet("sample3.dynamic_in", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}, Category ID: {row["category_id"]}");
        }
    }

    static void Dynamic_IN_Safe()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new Dictionary<string, object>()
        {
            { "categories", categoriesArray },
        };
        var cmd = dbAccess.CreateCommand("sample3.dynamic_in2", parameters);
        Console.WriteLine($"COMMAND TEXT: {cmd.CommandText}");
        DataSet ds = dbAccess.ExecuteCommandDataSet(cmd);
        //DataSet ds = dbAccess.ExecuteQueryDataSet("sample3.dynamic_in2", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}, Category ID: {row["category_id"]}");
        }
    }

    static void Dynamic_Update_Unsafe()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new Dictionary<string, object>
        {
            { "col_id", 9999 },
            { "col_str", "value_9999" },
            { "col_int", 199991 },
        };
        // 반복 수행을 위해 기존 데이터 삭제
        dbAccess.ExecuteSqlNonQuery("DELETE FROM my_demo_table WHERE col_id = 9999");
        // 테스트용 신규 데이터 삽입
        dbAccess.ExecuteSqlNonQuery("INSERT INTO my_demo_table (col_id, col_str, col_int) VALUES (9999, '', 0)");
        DumpDemoTable(dbAccess, "Before Update");
        var cmd = dbAccess.CreateCommand("sample3.dynamic_update", parameters);
        Console.WriteLine($"COMMAND TEXT: {cmd.CommandText}");
        dbAccess.ExecuteCommandNonQuery(cmd);
        //dbAccess.ExecuteQueryNonQuery("sample3.dynamic_update", parameters);
        DumpDemoTable(dbAccess, "After Update");
    }

    static void Dynamic_Update_Safe()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new Dictionary<string, object>
        {
            { "col_id", 9999 },
            { "col_str", "value_9999" },
            { "col_int", 199991 },
        };
        // 반복 수행을 위해 기존 데이터 삭제
        dbAccess.ExecuteSqlNonQuery("DELETE FROM my_demo_table WHERE col_id = 9999");
        // 테스트용 신규 데이터 삽입
        dbAccess.ExecuteSqlNonQuery("INSERT INTO my_demo_table (col_id, col_str, col_int) VALUES (9999, '', 0)");
        DumpDemoTable(dbAccess, "Before Update");
        var cmd = dbAccess.CreateCommand("sample3.dynamic_update2", parameters);
        Console.WriteLine($"COMMAND TEXT: {cmd.CommandText}");
        dbAccess.ExecuteCommandNonQuery(cmd);
        //dbAccess.ExecuteQueryNonQuery("sample3.dynamic_update2", parameters);
        DumpDemoTable(dbAccess, "After Update");
    }

    static void Dynamic_Insert_Unsafe()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new Dictionary<string, object>
        {
            { "col_id", 9999 },
            { "col_str", "value_9999" },
            { "col_int", 199991 },
        };
        // 반복 수행을 위해 기존 데이터 삭제
        dbAccess.ExecuteSqlNonQuery("DELETE FROM my_demo_table WHERE col_id = 9999");
        var cmd = dbAccess.CreateCommand("sample3.dynamic_insert", parameters);
        Console.WriteLine($"COMMAND TEXT: {cmd.CommandText}");
        dbAccess.ExecuteCommandNonQuery(cmd);
        //dbAccess.ExecuteQueryNonQuery("sample3.dynamic_insert", parameters);
        DumpDemoTable(dbAccess, "After Insert");
    }

    static void Dynamic_Insert_Safe()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new Dictionary<string, object>
        {
            { "col_id", 9999 },
            { "col_str", "value_9999" },
            { "col_int", 199991 },
        };
        // 반복 수행을 위해 기존 데이터 삭제
        dbAccess.ExecuteSqlNonQuery("DELETE FROM my_demo_table WHERE col_id = 9999");
        var cmd = dbAccess.CreateCommand("sample3.dynamic_insert2", parameters);
        Console.WriteLine($"COMMAND TEXT: {cmd.CommandText}");
        dbAccess.ExecuteCommandNonQuery(cmd);
        //dbAccess.ExecuteQueryNonQuery("sample3.dynamic_insert2", parameters);
        DumpDemoTable(dbAccess, "After Insert");
    }
}

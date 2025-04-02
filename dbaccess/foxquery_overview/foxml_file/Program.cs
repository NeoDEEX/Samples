using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using NeoDEEX.Data.Query;
using NeoDEEX.Security;
using System.Data;
using System.Data.Common;

namespace foxml_file;

internal class Program
{
    static void Main(string[] args)
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ConnectionStrings.ReloadConnectionStringFromConfig(config);

        ExecutingSimpleFoxQuery();
        //MissingDbParameterTest();
        //AmbientParameterDemo();
        //MacroDemo();
        //ReferencingParameter();
    }

    static void ExecutingSimpleFoxQuery()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new { product_id = 10, category_id = 1 };
        DataSet ds = dbAccess.ExecuteQueryDataSet("sample1:get_product", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}, Category: {row["category_id"]}");
        }
    }

    static void MissingDbParameterTest()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        Dictionary<string, object> parameters = new() { { "p0", "value0"}, { "p1", "value1" }, { "p2", 2 } };
        FoxQuery foxQuery = dbAccess.GetQuery("sample1:id1");
        IDbCommand cmd = dbAccess.CreateCommand(foxQuery, parameters);
        Console.WriteLine($"CommandText: {cmd.CommandText}"); 
        foreach (DbParameter p in cmd.Parameters)
        {
            Console.WriteLine($"Parameter: {p.ParameterName}  ({p.DbType})");
        }
        DataSet ds = dbAccess.ExecuteCommandDataSet(cmd);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"p0: {row["p0"]}, p1: {row["p1"]}, p2: {row["p2"]}");
        }
    }

    static void AmbientParameterDemo()
    {
        using var _ = FoxUserInfoContext.CreateScope(new FoxUserInfoContext("test_user"));
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        Dictionary<string, object> parameters = new() { { "msg", "Current user=" } };
        string? result = (string?)dbAccess.ExecuteQueryScalar("sample1:ambient_demo", parameters);
        Console.WriteLine($"Query result: {result}");
    }

    static void MacroDemo()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new { product_name = "%Queso%" };
        DataSet ds = dbAccess.ExecuteQueryDataSet("sample1.search_product", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }

    static void ReferencingParameter()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new { category_id = 3 };
        DataSet ds = dbAccess.ExecuteQueryDataSet("sample1:get_product_by_category", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}, Category: {row["category_id"]}");
        }
    }
}

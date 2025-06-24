using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using System.Data;

namespace foxquerymapper;

internal class Program
{
    static void Main()
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ExternalConfiguration = config;

        ExecutingSimpleFoxQuery();
    }

    static void ExecutingSimpleFoxQuery()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        string queryId = "northwind:get_product";
        var parameters = new { product_id = 3 };
        DataSet ds = dbAccess.ExecuteQueryDataSet(queryId, parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }
}

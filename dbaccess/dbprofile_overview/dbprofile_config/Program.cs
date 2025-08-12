using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using System.Data;

namespace dbprofile_config;

internal class Program
{
    static void Main()
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ExternalConfiguration = config;

        SimpleDbProfile();
        OracleAccess();
    }

    static void SimpleDbProfile()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        dbAccess.CallerName = nameof(SimpleDbProfile);
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        parameters.AddWithValue("p0", 3);
        DataSet ds = dbAccess.ExecuteSqlDataSet("SELECT * FROM products WHERE product_id < :p0", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }

    static void OracleAccess()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess("Oracle");
        object? result = dbAccess.ExecuteSqlScalar("SELECT COUNT(*) FROM products");
        Console.WriteLine($"Products record count: {result}");
    }
}

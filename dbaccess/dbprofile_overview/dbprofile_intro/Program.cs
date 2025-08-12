using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using System.Data;

namespace dbprofile_intro;

internal class Program
{
    static void Main()
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ExternalConfiguration = config;

        // 전역 설정: 동기적으로 로그를 기록하도록 진단 속성을 true로 설정
        FoxDatabaseConfig.DbProfileSettings.Diagnostics = true;

        SimpleDbProfile();
    }

    static void SimpleDbProfile()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        // 개별 FoxDbAccess 단위 FoxDbProfile 설정
        dbAccess.DbProfileSettings.Enabled = true;
        dbAccess.DbProfileSettings.LoggerName = "ConsoleLogger";

        dbAccess.CallerName = nameof(SimpleDbProfile);
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        parameters.AddWithValue("p0", 3);
        DataSet ds = dbAccess.ExecuteSqlDataSet("SELECT * FROM products WHERE product_id < :p0", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }
}

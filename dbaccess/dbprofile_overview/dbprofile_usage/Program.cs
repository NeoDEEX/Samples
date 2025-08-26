using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using System.Data;

namespace dbprofile_usage;

internal class Program
{
    static void Main()
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ExternalConfiguration = config;

        SimpleDbProfile();
        UpdateForGenerateDbProfileInfo();
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

    // DB Profile 정보를 생성하기 위해 데이터 변경을 수행하지 않는 UPDATE 문을 실행합니다.
    static void UpdateForGenerateDbProfileInfo()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        dbAccess.CallerName = nameof(UpdateForGenerateDbProfileInfo);
        DataSet ds = dbAccess.ExecuteSqlDataSet("SELECT * FROM products");
        Random rand = new();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            int productId = row.Field<int>("product_id");
            string? productName = row.Field<string>("product_name");
            if (productName == null)
            {
                continue;
            }
            if (rand.Next(100) < 20)
            {
                try
                {
                    dbAccess.ExecuteSqlNonQuery("SELECT * FROM NonExistTable");
                }
                catch
                {
                    // 무시
                }
                Console.WriteLine($"Generate ERROR profile info: product_id={productId}");
                continue;
            }
            FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
            parameters.AddWithValue("product_id", productId);
            // DbProfile 생성이 목적이므로 동일한 값 사용.
            parameters.AddWithValue("product_name", productName);
            int affectedRows = dbAccess.ExecuteSqlNonQuery("UPDATE products SET product_name = :product_name WHERE product_id = :product_id", parameters);
            Console.WriteLine($"Generate profile info: product_id={productId}, processed={affectedRows}");
        }
    }
}

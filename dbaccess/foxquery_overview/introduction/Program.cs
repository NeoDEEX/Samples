using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using NeoDEEX.Data.Query;
using System.Data;

namespace introduction;

internal class Program
{
    static readonly string FoxmlFilePath = "./foxml/postgresql/reload_test.foxml";

    static void Main(string[] args)
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ConnectionStrings.ReloadConnectionStringFromConfig(config);

        // Npgsql 7.x 이상 버전에서 함수를 저장 프로시저로 호출하기 위해 필요한 코드
        AppContext.SetSwitch("Npgsql.EnableStoredProcedureCompatMode", true);

        // Reload Foxml 테스트를 위해 foxml 파일을 생성합니다.
        CreateFoxmlForRelodTest();

        //ExecutingSimpleFoxQuery();
        SimpleObjectMapping();
        //ExecutingFoxQueryInDetail();
        //ExecutingSpUsingFoxQuery();
        //ReloadFoxmlShowCase();
        DynamicQuery("Queso");
        //DynamicQuery(null);
    }

    static void CreateFoxmlForRelodTest()
    {
        Stream stream = typeof(Program).Assembly.GetManifestResourceStream("introduction.resources.reload_test.foxml")!;
        if (stream == null)
        {
            throw new Exception("Resource not found.");
        }
        if (File.Exists(FoxmlFilePath) == true)
        {
            File.Delete(FoxmlFilePath);
        }
        using FileStream fs = new(FoxmlFilePath, FileMode.Create, FileAccess.Write);
        stream.CopyTo(fs);
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

    static void SimpleObjectMapping()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new { product_id = 3 };
        List<Product> products = dbAccess.ExecuteQueryList<Product>("northwind:get_product", parameters);
        foreach (Product product in products)
        {
            Console.WriteLine($"Product ID: {product.Product_Id}, Product Name: {product.Product_Name}");
        }
    }

    static void ExecutingFoxQueryInDetail()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        // FoxQuery 정보를 읽는다.
        string queryId = "northwind:get_product";
        FoxQuery foxQuery = dbAccess.GetQuery(queryId);
        // FoxQuery 객체는 foxml 에 기록된 쿼리 정보를 담고 있다.
        Console.WriteLine($"SQL Text: {foxQuery.Text}");
        Console.WriteLine($"DB Parameters: {foxQuery.Parameters.Count}");
        foreach(var pInfo in foxQuery.Parameters)
        {
            Console.WriteLine($"    Name: {pInfo.Name}, DbTypeName: {pInfo.DbTypeName}");
        }
        // FoxQuery 정보로부터 Command 객체를 생성한다.
        var parameters = new { product_id = 3 };
        IDbCommand cmd = dbAccess.CreateCommand(foxQuery, parameters);
        // Command 객체를 이용하여 쿼리를 실행한다.
        List<Product> products = dbAccess.ExecuteCommandList<Product>(cmd);
        foreach (Product product in products)
        {
            Console.WriteLine($"Product ID: {product.Product_Id}, Product Name: {product.Product_Name}");
        }
    }

    static void ExecutingSpUsingFoxQuery()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        DataSet ds = dbAccess.ExecuteQueryDataSet("northwind:get_categories");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Category ID: {row["category_id"]}, Category Name: {row["category_name"]}");
        }
    }

    static void ReloadFoxmlShowCase()
    {
        string queryId = "reload_test:get_product";
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess("PostgreSQL");
        // Foxml 변경 전 FoxQuery 수행
        DataSet ds = dbAccess.ExecuteQueryDataSet(queryId);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
        Console.WriteLine("----------------------------------------");
        // Foxml 파일 변경
        Console.WriteLine("Modifying foxml file & re executing query");
        string foxmlContent = File.ReadAllText(FoxmlFilePath);
        foxmlContent = foxmlContent.Replace("product_id < 3", "product_id <= 3");
        File.WriteAllText(FoxmlFilePath, foxmlContent);
        // Foxml reload 는 약간의 시간이 소요되므로 시간 지연
        Thread.Sleep(500);
        // 동일한 FoxQuery 수행 후 Foxml 파일이 다시 로드 되었음을 확인
        ds = dbAccess.ExecuteQueryDataSet(queryId);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }

    static void DynamicQuery(string? keyword)
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        Dictionary<string, object?> parameters = new() { { "product_name", null } };
        if (keyword != null)
        {
            parameters["product_name"] = "%" + keyword + "%";
        }
        DataSet ds = dbAccess.ExecuteQueryDataSet("northwind.search_product", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }
}

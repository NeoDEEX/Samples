using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using NeoDEEX.Data.NpgsqlClient;
using NeoDEEX.Data.OracleClient;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Diagnostics;

namespace creating_foxdbaccess;

internal class Program
{
#pragma warning disable IDE0051 // Remove unused private members

    static void Main()
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ConnectionStrings.ReloadConnectionStringFromConfig(config);
        npgsqlConnectionString = FoxDatabaseConfig.ConnectionStrings["PostgreSQL"]!.ConnectionString;

        //CreateConcreteClass();
        //UsingFactoryPattern();
        //CreatingSpecificDbAccess();
        AccessingConcreteDbAccess();
    }

    static string npgsqlConnectionString = "... your_connection_string ...";

    static void CreateConcreteClass()
    {
        FoxNpgsqlDbAccess dbAccess = new(npgsqlConnectionString);
        NpgsqlParameter[] parameters =
        [
            new NpgsqlParameter("product_id", 20),
            new NpgsqlParameter("product_name", "A%")
        ];
        string query = "SELECT * FROM products WHERE product_id <= :product_id AND product_name LIKE :product_name";
        DataSet ds = dbAccess.ExecuteSqlDataSet(query, parameters);
        foreach(DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }

    static void UsingFactoryPattern()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        Debug.Assert(dbAccess is FoxNpgsqlDbAccess);
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        Debug.Assert(parameters is FoxNpgsqlParameterCollection);
        parameters.AddWithValue("product_id", DbType.Int16, 20);
        parameters.AddWithValue("product_name", DbType.String, "A%");
        string query = "SELECT * FROM products WHERE product_id <= :product_id AND product_name LIKE :product_name";
        DataSet ds = dbAccess.ExecuteSqlDataSet(query, parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }

    static void CreatingSpecificDbAccess()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess("Oracle");
        Debug.Assert(dbAccess is FoxOracleDbAccess);
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        Debug.Assert(parameters is FoxOracleParameterCollection);
        parameters.AddWithValue("product_id", DbType.Int16, 20);
        parameters.AddWithValue("product_name", DbType.String, "A%");
        string query = "SELECT * FROM products WHERE product_id <= :product_id AND product_name LIKE :product_name";
        DataSet ds = dbAccess.ExecuteSqlDataSet(query, parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }

    // PostgreSQL 테스트용 함수 생성 스크립트
    //
    // CREATE OR REPLACE FUNCTION get_products_with_param(p_id integer, p_name varchar)
    //     RETURNS refcursor AS $$
    // DECLARE
    //     refcur  REFCURSOR;
    // BEGIN
    //     Open refcur for
    // 		SELECT product_id, product_name       
    //         FROM products WHERE product_id <= p_id AND product_name like p_name;
    // 	return refcur;
    // END; 
    // $$ LANGUAGE plpgsql;
    //
    static void AccessingConcreteDbAccess()
    {
        // Npgsql 7.0 부터 CommandType.StoredProcedure 가 저장 프로시저 호출로 설정된다.
        // 이전 버전 처럼 함수 호출로 바꾸어준다.
        AppContext.SetSwitch("Npgsql.EnableStoredProcedureCompatMode", true);
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        parameters.AddWithValue("p_id", DbType.Int32, 20);
        parameters.AddWithValue("p_name", DbType.String, "A%");
        string functionName = "get_products_with_param";
        DataSet ds = ((FoxNpgsqlDbAccess)dbAccess).ExecuteSpDataSet3(functionName, (FoxNpgsqlParameterCollection)parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }
}

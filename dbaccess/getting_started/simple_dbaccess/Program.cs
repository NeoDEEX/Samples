using NeoDEEX.Data;
using NeoDEEX.Data.NpgsqlClient;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Diagnostics;

namespace simple_dbaccess;

internal class Program
{
    const string DB_ConnectionString = "Server=test-prostgresql.postgres.database.azure.com;Database=testdb;Port=5432;User Id=tester;Password=test;Ssl Mode=Require;";

    static void Main(string[] args)
    {
        //SimpleDbAccess();
        //ParameterizedDbAccess();
        //SingleApiDbAccess();
        FoxQueryDbAccess();
    }

    static void SimpleDbAccess()
    {
        string connectionString = DB_ConnectionString;
        string query = "SELECT * FROM products WHERE product_id < 5";
        using FoxNpgsqlDbAccess dbAccess = new(connectionString);
        DataSet ds = dbAccess.ExecuteSqlDataSet(query);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"product_id: {row["product_id"]}, product_name: {row["product_name"]}");
        }
    }

    static void ParameterizedDbAccess()
    {
        string connectionString = DB_ConnectionString;
        string query = "SELECT * FROM products WHERE product_id < :product_id";
        FoxNpgsqlParameterCollection parameters = [];   // new();
        parameters.AddWithValue("product_id", NpgsqlDbType.Integer, 5);

        using FoxNpgsqlDbAccess dbAccess = new(connectionString);
        DataSet ds = dbAccess.ExecuteSqlDataSet(query, parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"product_id: {row["product_id"]}, product_name: {row["product_name"]}");
        }
    }

    static void SingleApiDbAccess()
    {
        string query = "SELECT * FROM products WHERE product_id < :product_id";
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();

        parameters.AddWithValue("product_id", DbType.Int32, 4);
        DataSet ds = dbAccess.ExecuteSqlDataSet(query, parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"product_id: {row["product_id"]}, product_name: {row["product_name"]}");
        }
    }

    static void FoxQueryDbAccess()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        Dictionary<string, object> parameters = new() {
            { "product_id", 5 }
        };
        // POCO 매개변수 예제
        //var parameters = new { product_id = 2 };
        DataSet ds = dbAccess.ExecuteQueryDataSet("northwind.get_product", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"product_id: {row["product_id"]}, product_name: {row["product_name"]}");
        }
    }
}

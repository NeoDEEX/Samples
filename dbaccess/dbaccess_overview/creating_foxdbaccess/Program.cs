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
    static void Main(string[] args)
    {
        //CreateConcreteClass();
        //UsingFactoryPattern();
        CreatingSpecificDbAccess();
    }

    static readonly string npgsqlConnectionString = "... your_connection_string ...";

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
}

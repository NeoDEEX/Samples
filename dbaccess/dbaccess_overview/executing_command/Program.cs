using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using NeoDEEX.Data.NpgsqlClient;
using NeoDEEX.Data.Query;
using Npgsql;
using System.Data;

namespace executing_command;

internal class Program
{
#pragma warning disable IDE0051 // Remove unused private members

    static void Main()
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ConnectionStrings.ReloadConnectionStringFromConfig(config);

        //string dbName = "PostgreSQL";
        //DeleteTestData(dbName);
        ExecutingSqlDataSet();
        //CreateCommandAndExecute();
        ExecutingExternalCommand();
        //CreatingFoxQueryCommandAndExecute();
        //ExecutingFoxQuery();
        //ExecutingSqlList();
        //ExecutingSqlNonQuery(dbName);
        //ExecutingSqlScalar();
        //ExecutingSpReader();
        //ExecutingReaderWithCustomCommandBehavior();
        //ExecutingTwoQueriesIntoDataSet();
        //CollectResultsIntoDataSet();
        //ExecutingFoxQueryWithParameter();
        //CreatingFoxQueryCommandAndExecute();
        //DefaultTableNames();
        //DataTableNameMapping();
    }

    static void DeleteTestData(string dbName)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(dbName);
        string query = "DELETE FROM my_demo_table WHERE col_id > 3";
        int affectedRows = dbAccess.ExecuteSqlNonQuery(query);
        Console.WriteLine($"{affectedRows} rows deleted.");
    }

    static void ExecutingSqlDataSet()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        string query = "SELECT * FROM products WHERE product_id < 3";
        DataSet ds = dbAccess.ExecuteSqlDataSet(query);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }

    static void CreateCommandAndExecute()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();

        string query = "SELECT * FROM products WHERE product_id <= :product_id AND product_name LIKE :product_name";
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        parameters.AddWithValue("product_id", DbType.Int16, 20);
        parameters.AddWithValue("product_name", DbType.String, "A%");

        //IDbCommand command = dbAccess.CreateCommand(query, CommandType.Text, parameters);
        //DataSet ds = dbAccess.ExecuteCommandDataSet(command);
        DataSet ds = dbAccess.ExecuteCommandDataSet(query, CommandType.Text, parameters);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }

    // 이 메서드 수행 전에 my_demo_table 테이블을 생성하고 적절한 데이터를 삽입해야 함.
    // (솔루션 폴더의 하위 schema 폴더 내의 my_demo_table.sql 파일 참조)
    // 또한 99번 id를 가진 데이터가 존재하면 안됨.
    static void ExecutingExternalCommand()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        IDbDataAdapter adapter = dbAccess.CreateDataAdapter();
        adapter.SelectCommand = dbAccess.CreateCommand("SELECT * FROM my_demo_table", CommandType.Text);
        NpgsqlCommandBuilder builder = new((NpgsqlDataAdapter)adapter);
        NpgsqlCommand command = builder.GetInsertCommand();
        command.Parameters["p1"].Value = 99;
        command.Parameters["p2"].Value = "str99";
        command.Parameters["p3"].Value = 99;
        dbAccess.ExecuteCommandNonQuery(command);

        DataSet ds = dbAccess.ExecuteCommandDataSet(adapter.SelectCommand);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"id: {row["col_id"]}, str: {row["col_str"]}, int: {row["col_int"]}");
        }
    }

    static void ExecutingFoxQuery()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        DataSet ds = dbAccess.ExecuteQueryDataSet("postgre.select_categories");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Category ID: {row["category_id"]}, Category Name: {row["category_name"]}");
        }
    }

    static void ExecutingSqlList()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        List<Product> products = dbAccess.ExecuteSqlList<Product>("SELECT * FROM products WHERE product_id < 3");
        foreach (Product product in products)
        {
            Console.WriteLine($"Product ID: {product.Product_Id}, Product Name: {product.Product_Name}");
        }
    }

    static void ExecutingSqlNonQuery(string dbName)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(dbName);
        string query = "INSERT INTO my_demo_table VALUES(:id, :str, :int)";
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        parameters.AddWithValue("id", 99);
        parameters.AddWithValue("str", "str99");
        parameters.AddWithValue("int", 99);
        int affectedRows = dbAccess.ExecuteSqlNonQuery(query, parameters);
        Console.WriteLine($"Affected Rows: {affectedRows}");
    }

    static void ExecutingSqlScalar()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess("Oracle");
        string query = "SELECT COUNT(*) FROM products";
        object? result = dbAccess.ExecuteSqlScalar(query);
        int count = Convert.ToInt32(result);
        Console.WriteLine($"Count: {count}");
    }

    static void ExecutingSqlReader()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        string query = "SELECT * FROM categories WHERE category_id <= 5";
        using IDataReader reader = dbAccess.ExecuteSpReader(query);
        while (reader.Read())
        {
            Console.WriteLine($"Category ID: {reader["category_id"]}, Category Name: {reader["category_name"]}");
        }
    }

    static void ExecutingReaderWithCustomCommandBehavior()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        string query = "SELECT * FROM products WHERE product_id > 5";
        IDbCommand command = dbAccess.CreateCommand(query, CommandType.Text);
        dbAccess.Open();
        try
        {
            using IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
            while (reader.Read())
            {
                Console.WriteLine($"Product ID: {reader["product_id"]}, Product Name: {reader["product_name"]}");
            }
        }
        finally
        {
            dbAccess.Close();
        }
    }

    static void ExecutingTwoQueriesIntoDataSet()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess("Oracle");
        string query1 = "SELECT * FROM products WHERE product_id < 3";
        string query2 = "SELECT * FROM categories WHERE category_id < 3";
        DataSet ds = new();
        dbAccess.Open();
        try
        {
            dbAccess.ExecuteSql(query1, ["products"], ds);
            dbAccess.ExecuteSql(query2, ["categories"], ds);
        }
        finally
        {
            dbAccess.Close();
        }
        foreach (DataRow row in ds.Tables["products"]!.Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
        foreach (DataRow row in ds.Tables["categories"]!.Rows)
        {
            Console.WriteLine($"Category ID: {row["category_id"]}, Category Name: {row["category_name"]}");
        }
    }

    static void CollectResultsIntoDataSet()
    {
        using FoxDbAccess dbAccess1 = FoxDbAccess.CreateDbAccess("PostgreSQL");
        using FoxDbAccess dbAccess2 = FoxDbAccess.CreateDbAccess("Oracle");
        string query1 = "SELECT * FROM products WHERE product_id < 3";
        string query2 = "SELECT * FROM categories WHERE category_id < 3";
        DataSet ds = new();
        dbAccess1.ExecuteSql(query1, ["products"], ds);
        dbAccess2.ExecuteSql(query2, ["categories"], ds);
        foreach (DataRow row in ds.Tables["products"]!.Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
        foreach (DataRow row in ds.Tables["categories"]!.Rows)
        {
            Console.WriteLine($"Category ID: {row["category_id"]}, Category Name: {row["category_name"]}");
        }
    }

    static void ExecutingFoxQueryWithParameter()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        Dictionary<string, object?> parameters = new()
        {
            { "product_id", 20 },
            { "product_name", "A%" }
        };
        //var parameters = new { product_id = 20, product_name = "A%" };
        DataSet ds = dbAccess.ExecuteQueryDataSet("postgre.select_products", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }

    static void CreatingFoxQueryCommandAndExecute()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        Dictionary<string, object?> parameters = new()
        {
            { "product_id", 20 },
            { "product_name", "A%" }
        };
        FoxQuery foxquery = dbAccess.GetQuery("postgre.select_products");
        using IDbCommand command = dbAccess.CreateCommand(foxquery, parameters);
        foreach(IDataParameter p in command.Parameters)
        {
            Console.WriteLine($"Parameters[\"{p.ParameterName}\"].Value = {p.Value}");
        }
        DataSet ds = dbAccess.ExecuteCommandDataSet(command);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
    }


    static void DefaultTableNames()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        string query= "SELECT * FROM products WHERE product_id < 3; SELECT * FROM categories WHERE category_id < 3";

        DataSet ds1 = dbAccess.ExecuteSqlDataSet(query);
        Console.WriteLine($"Table[0].TableName={ds1.Tables[0].TableName}");
        Console.WriteLine($"Table[1].TableName={ds1.Tables[1].TableName}");
        foreach (DataRow row in ds1.Tables["Table"]!.Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
        foreach (DataRow row in ds1.Tables["Table1"]!.Rows)
        {
            Console.WriteLine($"Category ID: {row["category_id"]}, Category Name: {row["category_name"]}");
        }

        dbAccess.DefaultTableName = "ResultSet";
        DataSet ds2 = dbAccess.ExecuteSqlDataSet(query);
        Console.WriteLine($"Table[0].TableName={ds2.Tables[0].TableName}");
        Console.WriteLine($"Table[1].TableName={ds2.Tables[1].TableName}");
    }

    static void DataTableNameMapping()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        string query = "SELECT * FROM products WHERE product_id < :product_id; SELECT * FROM categories WHERE category_id < :category_id";
        string[] mappingNames = ["Products", "Categories"];

        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        parameters.AddWithValue("product_id", DbType.Int16, 3);
        parameters.AddWithValue("category_id", DbType.Int16, 3);

        DataSet ds1 = dbAccess.ExecuteSqlDataSet(query, mappingNames, parameters);
        Console.WriteLine($"Table[0].TableName={ds1.Tables[0].TableName}");
        Console.WriteLine($"Table[1].TableName={ds1.Tables[1].TableName}");
        foreach (DataRow row in ds1.Tables["Products"]!.Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}");
        }
        foreach (DataRow row in ds1.Tables["Categories"]!.Rows)
        {
            Console.WriteLine($"Category ID: {row["category_id"]}, Category Name: {row["category_name"]}");
        }
    }
}

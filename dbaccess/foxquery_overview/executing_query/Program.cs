using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using NeoDEEX.Data.Query;
using System.Data;

namespace executing_query;

internal class Program
{
    static readonly int TestBaseId = 90;

    static void Main(string[] args)
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ConnectionStrings.ReloadConnectionStringFromConfig(config);

        //DetailedExecuteQuery();
        SimpleExecuteQuery();

        //BuildTestEnvironment();
        //DataTableOperationWithFoxQuery();
        //AnnoymousObjectArguements();
    }

    // ExecuteQuery- 시리즈 메서드가 작동하는 방식을 보여주는 에제 입니다.
    static void DetailedExecuteQuery()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        FoxQuery foxQuery = dbAccess.GetQuery("sample2.get_product");
        Dictionary<string, object> parameters = new() { { "product_id", 5 }, { "category_id", 1 } };
        IDbCommand command = dbAccess.CreateCommand(foxQuery, parameters);
        DataSet ds = dbAccess.ExecuteCommandDataSet(command);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}, Category: {row["category_id"]}");
        }
    }

    // 단순한 FoxQuery 수행 예제 입니다.
    static void SimpleExecuteQuery()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        Dictionary<string, object> parameters = new() { { "product_id", 5 }, { "category_id", 1 } };
        DataSet ds = dbAccess.ExecuteQueryDataSet("Sample2.get_product", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}, Category: {row["category_id"]}");
        }
    }

    // 반복적으로 예제 코드를 작동시기키 위해 테스트 환경을 구축합니다.
    static void BuildTestEnvironment()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        dbAccess.Open();
        try
        {
            dbAccess.BeginTrans();

            // 기존의 테스트 데이터를 모두 삭제합니다.
            Dictionary<string, object?> parameters = new() { { "col_id", TestBaseId }, { "col_str", null }, { "col_int", null } };
            parameters["col_id"] = TestBaseId;
            dbAccess.ExecuteQueryNonQuery("sample2.delete_test_data", parameters);
            // 3 건의 테스트 데이터를 삽입합니다.
            for (int i = 0; i < 3; i++)
            {
                int index = TestBaseId + i + 1;
                parameters["col_id"] = index;
                parameters["col_str"] = "test" + index;
                parameters["col_int"] = index;
                dbAccess.ExecuteQueryNonQuery("sample2.insert_demo", parameters); 
            }
            dbAccess.CommitTrans();
        }
        catch (FoxDbException)
        {
            if (dbAccess.IsInLocalTransaction == true)
            {
                dbAccess.RollbackTrans();
            }
            throw;
        }
        finally
        {
            dbAccess.Close();
        }
    }

    // DataRow 를 매개변수 인자로 사용하는 예제 입니다.
    // NOTE: 이 메서드 호출 전에 BuildTestEnvironment 메서드를 호출해야 합니다.
    static void DataTableOperationWithFoxQuery()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new { col_id = TestBaseId };

        dbAccess.Open();
        try
        {
            // 원본 테스트 데이터를 읽어 표시합니다.
            DataSet ds = dbAccess.ExecuteQueryDataSet("sample2.get_demo", parameters);
            DataTable dt = ds.Tables[0];
            DumpDemoTable("Original", dt);

            // DataTable 에 추가/수정/삭제를 수행합니다.
            dt.Rows[0]["col_str"] = "changed!";
            dt.Rows[1].Delete();
            int newId = TestBaseId + 9;
            DataRow newRow = dt.NewRow();
            newRow["col_id"] = newId;
            newRow["col_str"] = "new!";
            newRow["col_int"] = newId;
            dt.Rows.Add(newRow);

            dbAccess.BeginTrans();
            // DataTable 에서 변경된 내용을 DB에 반영합니다.
            foreach (DataRow row in dt.GetChanges()!.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    dbAccess.ExecuteQueryNonQuery("sample2.insert_demo", row);
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    dbAccess.ExecuteQueryNonQuery("sample2.update_demo", row);
                }
                else if (row.RowState == DataRowState.Deleted)
                {
                    dbAccess.ExecuteQueryNonQuery("sample2.delete_demo", row);
                }
            }
            dbAccess.CommitTrans();

            ds = dbAccess.ExecuteQueryDataSet("sample2.get_demo", parameters);
            DumpDemoTable("Modified", ds.Tables[0]);
        }
        catch(FoxDbException)
        {
            if (dbAccess.IsInLocalTransaction == true)
            {
                dbAccess.RollbackTrans();
            }
            throw;
        }
        finally
        {
            dbAccess.Close();
        }

    }

    // DataTable 의 내용을 덤프합니다.
    static void DumpDemoTable(string label, DataTable dt)
    {
        Console.WriteLine($">>> {label} data:");
        Console.WriteLine($"{"col_id",12}|{"col_str",12}|{"col_int",12}");
        Console.WriteLine($"{"------------",12}+{"------------",12}+{"------------",12}");
        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"{row["col_id"],12}|{row["col_str"],12}|{row["col_int"],12}");
        }
        Console.WriteLine();
    }

    // 익명 타입을 이용하여 매개변수 인자를 사용하는 예제 입니다.
    static void AnnoymousObjectArguements()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new { Product_Id = 30, Category_Id = 1 };
        DataSet ds = dbAccess.ExecuteQueryDataSet("Sample2.get_product", parameters);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"Product ID: {row["product_id"]}, Product Name: {row["product_name"]}, Category: {row["category_id"]}");
        }
    }

    // ORM 스타일 예제 입니다.
    static void OrmDemo()
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        Product parameters = new() { Product_Id = 30, Category_Id = 1 };
        List<Product> products = dbAccess.ExecuteQueryList<Product>("sample2.get_product", parameters);
        foreach (Product row in products)
        {
            Console.WriteLine($"Product ID: {row.Product_Id}, Product Name: {row.Product_Name}, Category: {row.Category_Id}");
        }
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Diagnostics;

namespace transaction_management;

internal class Program
{
    static void Main()
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ConnectionStrings.ReloadConnectionStringFromConfig(config);
        // 이전 수행에서 생성된 테스트 데이터를 삭제합니다.
        string connName = "PostgreSQL";
        DeleteTestData(connName);

        // 전통적인 트랜잭션 데이터액세스 코드
        string connectionString = FoxDatabaseConfig.ConnectionStrings[connName]!.ConnectionString;
        //LegacyPostgreSqlTransactionalCode(connectionString);
        //LegacyOracleTransactionalCode(connectionString);
        //LegacySqlServerTransactionalCode(connectionString);
        NeoDEEXTransationalCode(connName);
        DumpTestTable(connName);
    }

    static void DeleteTestData(string connName)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(connName);
        string query = "DELETE FROM my_demo_table WHERE col_id > 3";
        int affectedRows = dbAccess.ExecuteSqlNonQuery(query);
        Console.WriteLine($"{affectedRows} rows deleted.");
    }

    // PostgreSQL 의 트랜잭션 코드
    static void LegacyPostgreSqlTransactionalCode(string connectionString)
    {
        using NpgsqlConnection conn = new(connectionString);
        conn.Open();
        try
        {
            string query = "INSERT INTO my_demo_table VALUES(:id, :str, :int)";
            using NpgsqlCommand cmd1 = new(query, conn);
            cmd1.Parameters.AddWithValue("id", 98);
            cmd1.Parameters.AddWithValue("str", "str98");
            cmd1.Parameters.AddWithValue("int", 98);
            using NpgsqlCommand cmd2 = new(query, conn);
            cmd1.Parameters.AddWithValue("id", 99);
            cmd1.Parameters.AddWithValue("str", "str99");
            cmd1.Parameters.AddWithValue("int", 99);
            using NpgsqlTransaction tx = conn.BeginTransaction();
            cmd1.Transaction = tx;
            cmd2.Transaction = tx;
            try
            {
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                // Npgsql 에서 Tranaction 속성의 값은 무시되며 BeginTransaction 메서드 이후 수행되는
                // 모든 명령이 트랜잭션에 속하게 된다.
                //using NpgsqlCommand cmd3 = new("INSERT INTO my_demo_table VALUES(88, 'str88', 88)", conn);
                //cmd2.ExecuteNonQuery();

                //throw new InvalidOperationException("테스트용 오류...");

                tx.Commit();
                Console.WriteLine("Transaction Committed.");
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine($"Transaction Rollbacked: {ex.Message}");
            }
        }
        finally
        {
            conn.Close();
        }
    }

    // Oracle 의 트랜잭션 코드
    static void LegacyOracleTransactionalCode(string connectionString)
    {
        using OracleConnection conn = new(connectionString);
        conn.Open();
        try
        {
            string query = "INSERT INTO my_demo_table VALUES(:id, :str, :int)";
            using OracleCommand cmd1 = new(query, conn);
            cmd1.Parameters.Add("id", 98);
            cmd1.Parameters.Add("str", "str98");
            cmd1.Parameters.Add("int", 98);
            using OracleCommand cmd2 = new(query, conn);
            cmd1.Parameters.Add("id", 99);
            cmd1.Parameters.Add("str", "str99");
            cmd1.Parameters.Add("int", 99);
            using OracleTransaction tx = conn.BeginTransaction();
            cmd1.Transaction = tx;
            cmd2.Transaction = tx;
            try
            {
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                // Managed ODP.NET Core 에서 BeginTransaction 메서드 이후 수행되는
                // 모든 명령이 트랜잭션에 속하게 되며 Transaction 속성의 값은 사용되지 않는다.
                //using OracleCommand cmd3 = new("INSERT INTO my_demo_table VALUES(77, 'str77', 77)", conn);
                //cmd2.ExecuteNonQuery();

                //throw new InvalidOperationException("테스트용 오류...");

                tx.Commit();
                Console.WriteLine("Transaction Committed.");
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine($"Transaction Rollbacked: {ex.Message}");
            }
        }
        finally
        {
            conn.Close();
        }
    }

    // SQL Server 의 트랜잭션 코드
    static void LegacySqlServerTransactionalCode(string connectionString)
    {
        using SqlConnection conn = new(connectionString);
        conn.Open();
        try
        {
            string query = "INSERT INTO my_demo_table VALUES(@id, @str, @int)";
            using SqlCommand cmd1 = new(query, conn);
            cmd1.Parameters.AddWithValue("id", 98);
            cmd1.Parameters.AddWithValue("str", "str98");
            cmd1.Parameters.AddWithValue("int", 98);
            using SqlCommand cmd2 = new(query, conn);
            cmd2.Parameters.AddWithValue("id", 99);
            cmd2.Parameters.AddWithValue("str", "str99");
            cmd2.Parameters.AddWithValue("int", 99);
            using SqlTransaction tx = conn.BeginTransaction();
            cmd1.Transaction = tx;
            cmd2.Transaction = tx;
            try
            {
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                // Microsoft.Data.SqlClient 에서는 BeginTransaction() 메서드 호출 이후
                // Transaction 속성이 설정되지 않은 SqlCommand 객체를 사용하면
                // 예외가 발생한다.
                using SqlCommand cmd3 = new("INSERT INTO my_demo_table VALUES(88, 'str88', 88)", conn);
                cmd2.ExecuteNonQuery();

                //throw new InvalidOperationException("테스트용 오류...");

                tx.Commit();
                Console.WriteLine("Transaction Committed.");
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Console.WriteLine($"Transaction Rollbacked: {ex.Message}");
            }
        }
        finally
        {
            conn.Close();
        }
    }

    static void DumpTestTable(string connName)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(connName);
        string query = "SELECT * FROM my_demo_table";
        DataSet ds = dbAccess.ExecuteSqlDataSet(query);
        Console.WriteLine($"{"id",8}|{"str",16}");
        Console.WriteLine("--------+----------------");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"{row["col_id"],8}|{row["col_str"],16}");
        }
    }

    static void NeoDEEXTransationalCode(string connName)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        dbAccess.Open();
        try
        {
            string query = "INSERT INTO my_demo_table VALUES(:id, :str, :int)";
            FoxDbParameterCollection parameters1 = dbAccess.CreateParamCollection();
            parameters1.AddWithValue("id", 98);
            parameters1.AddWithValue("str", "str98");
            parameters1.AddWithValue("int", 98);
            FoxDbParameterCollection parameters2 = dbAccess.CreateParamCollection();
            parameters2.AddWithValue("id", 99);
            parameters2.AddWithValue("str", "str99");
            parameters2.AddWithValue("int", 99);
            dbAccess.BeginTrans();
            Console.WriteLine($"FoxDbAccess.Transaction: {dbAccess.DbTransaction}");
            try
            {
                Debug.Assert(dbAccess.IsInLocalTransaction);
                dbAccess.ExecuteSqlNonQuery(query, parameters1);
                dbAccess.ExecuteSqlNonQuery(query, parameters2);

                //dbAccess.ExecuteSqlNonQuery("INSERT INTO my_demo_table VALUES(88, 'str88', 88)");

                //throw new InvalidOperationException("테스트용 오류...");

                dbAccess.CommitTrans();
            }
            catch (Exception ex)
            {
                dbAccess.RollbackTrans();
                Console.WriteLine($"Transaction Rollbacked: {ex.Message}");
            }
        }
        finally
        {
            dbAccess.Close();
        }
    }
}

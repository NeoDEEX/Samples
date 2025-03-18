using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using Npgsql;
using NpgsqlTypes;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;

namespace exception_handling;

internal class Program
{
    static void Main(string[] args)
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ConnectionStrings.ReloadConnectionStringFromConfig(config);

        // 이전 수행에서 생성된 테스트 데이터를 삭제합니다.
        string connName = "Oracle";

        //NpgsqlExceptionHandling();
        //OracleExceptionHandling();
        //SqlServerExceptionHandling();
        NeoDEEXExceptionHandling(connName);
    }

    static void NpgsqlExceptionHandling()
    {
        using NpgsqlConnection conn = new(FoxDatabaseConfig.ConnectionStrings["PostgreSQL"]!.ConnectionString);
        string query = "SELECT * FROM nonexist_table";
        using NpgsqlCommand cmd = new(query, conn);
        conn.Open();
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch(NpgsqlException ex)
        {
            Console.WriteLine($"DB 오류: {ex.Message}");
            Console.WriteLine(ex.ErrorCode);
        }
        finally
        {
            conn.Close();
        }
    }

    static void OracleExceptionHandling()
    {
        string connectionString = FoxDatabaseConfig.ConnectionStrings["Oracle"]!.ConnectionString;
        using OracleConnection conn = new(connectionString);
        string query = "SELECT COUNT(*) FROM nonexist_table";
        using OracleCommand cmd = new(query, conn);
        conn.Open();
        try
        {
            object result = cmd.ExecuteScalar();
            Console.WriteLine($"Result: {result}");
        }
        catch (OracleException ex)
        {
            Console.WriteLine($"DB 오류: {ex.Message}");
            Console.WriteLine(ex.ErrorCode);
        }
        finally
        {
            conn.Close();
        }
    }

    static void SqlServerExceptionHandling()
    {
        string connectionString = FoxDatabaseConfig.ConnectionStrings["SqlServer"]!.ConnectionString;
        using SqlConnection conn = new(connectionString);
        string query = "SELECT COUNT(*) FROM nonexist_table";
        using SqlCommand cmd = new(query, conn);
        conn.Open();
        try
        {
            object result = cmd.ExecuteScalar();
            Console.WriteLine($"Result: {result}");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"DB 오류: {ex.Message}");
            Console.WriteLine(ex.ErrorCode);
        }
        finally
        {
            conn.Close();
        }
    }

    static void NeoDEEXExceptionHandling(string connName)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(connName);
        string query = "SELECT * FROM nonexist_table";
        try
        {
            DataSet ds = dbAccess.ExecuteSqlDataSet(query);
        }
        catch(FoxDbException ex)
        {
            Console.WriteLine(ex.ToString());
            //Console.WriteLine($"DB 오류: {ex.Message}");
            Console.WriteLine(ex.ErrorCode);
        }
    }
}

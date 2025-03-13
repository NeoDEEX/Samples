using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace db_parameters;

internal class Program
{
    static void Main(string[] args)
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 재설정합니다.
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ConnectionStrings.ReloadConnectionStringFromConfig(config);

        // 이전 수행에서 생성된 테스트 데이터를 삭제합니다.
        string connName = "PostgreSQL";
        DeleteTestData(connName);

        //LegacyDbParameterManagement(connName);
        FoxDbAccessParameterManagement(connName);
        DumpTestTable(connName);
    }

    static void DeleteTestData(string connName)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(connName);
        string query = "DELETE FROM my_demo_table WHERE col_id > 3";
        int affectedRows = dbAccess.ExecuteSqlNonQuery(query);
        Console.WriteLine($"{affectedRows} rows deleted.");
    }

    static void DumpTestTable(string connName)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(connName);
        string query = "SELECT * FROM my_demo_table";
        DataSet ds = dbAccess.ExecuteSqlDataSet(query);
        Console.WriteLine($"{"id",8}|{"str",16}|{"int",8}");
        Console.WriteLine("--------+----------------+--------");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Console.WriteLine($"{row["col_id"],8}|{row["col_str"],16}|{row["col_int"],8}");
        }
    }

    static void LegacyDbParameterManagement(string connName)
    {
        using NpgsqlConnection conn = new(FoxDatabaseConfig.ConnectionStrings[connName]!.ConnectionString);
        string query = "INSERT INTO my_demo_table VALUES(:id, :str, :int)";
        using NpgsqlCommand cmd = new(query, conn);
        cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, 97);
        cmd.Parameters.AddWithValue("str", NpgsqlDbType.Varchar, DBNull.Value);
        cmd.Parameters.AddWithValue("int", NpgsqlDbType.Integer, 97);
        Console.WriteLine(cmd.Parameters["str"]!.Value);
        conn.Open();
        try
        {
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, 98);
            cmd.Parameters.AddWithValue("str", NpgsqlDbType.Varchar, "str98");
            cmd.Parameters.AddWithValue("int", NpgsqlDbType.Integer, DBNull.Value);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, 99);
            cmd.Parameters.AddWithValue("str", NpgsqlDbType.Varchar, "str99");
            cmd.Parameters.AddWithValue("int", NpgsqlDbType.Integer, 99);
            cmd.ExecuteNonQuery();

            //using NpgsqlCommand cmd = new(query, conn);
            //for (int i = 0; i < 3; i++)
            //{
            //    int index = 99 - i;
            //    cmd.Parameters.AddWithValue("id", NpgsqlDbType.Integer, index);
            //    cmd.Parameters.AddWithValue("str", NpgsqlDbType.Varchar, $"str{index}");
            //    cmd.Parameters.AddWithValue("int", NpgsqlDbType.Integer, index);
            //    cmd.ExecuteNonQuery();
            //    cmd.Parameters.Clear();
            //}
        }
        finally
        {
            conn.Close();
        }
    }

    static void FoxDbAccessParameterManagement(string connName)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(connName);
        string query = "INSERT INTO my_demo_table VALUES(:id, :str, :int)";
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        parameters.UseDuplicateCheck = false;
        Console.WriteLine($"parameters type: {parameters.GetType().FullName}");
        parameters.AddWithValue("id", DbType.Int32, 97);
        parameters.AddWithValue("str", DbType.String, null);
        parameters.AddWithValue("int", DbType.Int32, 97);
        dbAccess.Open();
        try
        {
            dbAccess.ExecuteSqlNonQuery(query, parameters.Clone());
            parameters["id"]!.Value = 98;
            parameters["str"]!.Value = "str98";
            parameters["int"]!.Value = null;
            dbAccess.ExecuteSqlNonQuery(query, parameters.Clone());
            parameters["id"]!.Value = 99;
            parameters["str"]!.Value = "str99";
            parameters["int"]!.Value = 99;
            dbAccess.ExecuteSqlNonQuery(query, parameters);

            //for (int i = 0; i < 3; i++)
            //{
            //    int index = 97 + i;
            //    parameters["id"]!.Value = index;
            //    parameters["str"]!.Value = $"str{index}";
            //    parameters["int"]!.Value = index;
            //    dbAccess.ExecuteSqlNonQuery(query, parameters.Clone());
            //}
        }
        finally
        {
            dbAccess.Close();
        }
    }
}

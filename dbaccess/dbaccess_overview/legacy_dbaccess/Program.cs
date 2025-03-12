using Microsoft.Extensions.Configuration;
using Npgsql;

namespace legacy_dbaccess;

internal class Program
{
    static readonly string ConnectionString;

    static Program()
    {
        // 연결 문자열 노출을 막기 위해 user-secrets 에서 연결 문자열을 읽어 설정합니다.
        IConfiguration config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        string key = "ConnectionStrings:PostgreSQL";
        ConnectionString = config[key];
    }

    static void Main(string[] args)
    {
        string query = "SELECT COUNT(*) FROM products";
        using NpgsqlConnection conn = new(ConnectionString);
        using NpgsqlCommand cmd = new(query, conn);
        conn.Open();
        try
        {
            long count = (long)cmd.ExecuteScalar()!;
            Console.WriteLine($"Number of products: {count}");
        }
        finally
        {
            conn.Close();
        }
    }
}

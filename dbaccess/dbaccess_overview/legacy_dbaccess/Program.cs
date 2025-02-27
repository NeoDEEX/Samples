using Npgsql;

namespace legacy_dbaccess;

internal class Program
{
    static readonly string ConnectionString = "... your_connection_string ...";

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

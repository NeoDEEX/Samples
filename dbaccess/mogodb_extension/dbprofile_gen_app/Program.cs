using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NeoDEEX.Data;
using NeoDEEX.Diagnostics;
using NeoDEEX.Extensions.Mongodb;
using Spectre.Console;
using System.Data;

namespace dbprofile_gen_app;

internal class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[blue]DbProfile Generation App[/] to [green]MongoDB[/]");

        FoxDatabaseConfig.AddUserSecrets(builder => builder.AddUserSecrets<Program>());

        // 데이터베이스를 액세스하여 DbProfileInfo 항목 생성
        //AnsiConsole.MarkupLine("Generating DbProfileInfo items...");
        //int generated = GenerateDbProfileInfo();

        // 최근에 생성된 DbProfileInfo 항목을 읽어 표시
        //AnsiConsole.MarkupLine("[blue]Generated {generated} DbProfileInfo items[/]...");
        //ReadDbProfileInfo(generated);

        // 지정된 기간 동안 생성된 DbProfileInfo 항목을 읽어 표시
        GetDbProfileInfo(new DateTime(2024, 8, 10), new DateTime(2025, 8, 26));
    }

    static int GenerateDbProfileInfo()
    {
        AnsiConsole.MarkupLine("Generating [blue]DbProfileInfo[/]...");
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        dbAccess.CallerName = nameof(GenerateDbProfileInfo);
        DataSet ds = dbAccess.ExecuteSqlDataSet("SELECT * FROM products");
        Random rand = new();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            int productId = row.Field<int>("product_id");
            string? productName = row.Field<string>("product_name");
            if (productName == null)
            {
                continue;
            }
            if (rand.Next(100) < 20)
            {
                try
                {
                    dbAccess.ExecuteSqlNonQuery("SELECT * FROM NonExistTable");
                }
                catch
                {
                    // 예외 무시
                }
                AnsiConsole.MarkupLine($"Generate [red]ERROR[/] profile info: [red]product_id={productId}[/]");
                continue;
            }
            FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
            parameters.AddWithValue("product_id", productId);
            // DbProfile 생성이 목적이므로 동일한 값 사용.
            parameters.AddWithValue("product_name", productName);
            string updateQuery = "UPDATE products SET product_name = :product_name WHERE product_id = :product_id";
            int affectedRows = dbAccess.ExecuteSqlNonQuery(updateQuery, parameters);
            AnsiConsole.MarkupLine($"Generate [green]SUCCESS[/] profile info: [green]product_id={productId}[/], processed={affectedRows}");
        }
        FlushDbProfile(dbAccess);
        return ds.Tables[0].Rows.Count + 1;
    }

    static void FlushDbProfile(FoxDbAccess dbAccess)
    {
        if (dbAccess.DbProfileSettings.Enabled == true
            && String.IsNullOrWhiteSpace(dbAccess.DbProfileSettings.LoggerName) == false)
        {
            IFoxLog log = FoxLogManager.GetLogger(dbAccess.DbProfileSettings.LoggerName);
            if (log is DbProfileLogger dbLogger)
            {
                AnsiConsole.MarkupLine($"[olive]Flushing remaining [yellow]{dbLogger.QueuedCount}[/] log items...[/]");
                dbLogger.FlushQueue();
                AnsiConsole.MarkupLine($"[green]{dbLogger.InsertedCount} log items[/] inserted.");
            }
        }
    }

    // 최종적으로 기록된 DbProfileInfo 항목들을 읽어와 출력합니다.
    static void ReadDbProfileInfo(int takes = 0)
    {
        AnsiConsole.Markup("Reading Recent [blue]DbProfileInfo[/]");
        if (takes > 0)
        {
            AnsiConsole.MarkupLine($" (Top [green]{takes}[/])...");
        }
        else
        {
            AnsiConsole.MarkupLine(" (All)...");
        }
        string connectionString = FoxDatabaseConfig.ExternalConfiguration!["connectionStrings:MongoDb"] 
            ?? throw new InvalidOperationException("MongoDB connection string is not configured.");
        using MongoClient client = MongoClientCache.GetClientFromCache(connectionString);
        IMongoCollection<DbProfileInfo> collection = client.GetDatabase("test_db")
            .GetCollection<DbProfileInfo>("dbprofile");
        var filter = Builders<DbProfileInfo>.Filter.Empty;
        var documents = takes > 0 
            ? collection.Find(filter).SortByDescending(d => d.Timestamp).Limit(takes).ToList() 
            : collection.Find(filter).SortByDescending(d => d.Timestamp).ToList();
        AnsiConsole.MarkupLine($"[blue]Read {documents.Count} DbProfileInfo items[/]...");
        Dump(documents);
    }

    static void GetDbProfileInfo(DateTime from, DateTime to)
    {
        to = to.AddDays(1).AddMicroseconds(-1);
        AnsiConsole.MarkupLine($"Getting [blue]DbProfileInfo[/] from [green]{from:yyyy-MM-dd}[/] to [green]{to:yyyy-MM-dd}[/]...");
        string connectionString = FoxDatabaseConfig.ExternalConfiguration!["connectionStrings:MongoDb"]
            ?? throw new InvalidOperationException("MongoDB connection string is not configured.");
        using var client = MongoClientCache.GetClientFromCache(connectionString);
        var collection = client.GetDatabase("test_db").GetCollection<DbProfileInfo>("dbprofile");

        var filter = Builders<DbProfileInfo>.Filter.And(
            Builders<DbProfileInfo>.Filter.Gte(d => d.Timestamp, from.ToUniversalTime()),
            Builders<DbProfileInfo>.Filter.Lte(d => d.Timestamp, to.ToUniversalTime())
        );
        var documents = collection.Find(filter).SortByDescending(d => d.Timestamp).Limit(1000).ToList();
        AnsiConsole.MarkupLine($"[blue]Read {documents.Count} DbProfileInfo items[/]...");
        Dump(documents);
    }

    static void Dump(IEnumerable<DbProfileInfo> documents)
    {
        int count = documents.Count();
        if (count <= 10)
        {
            DumpAll(documents);
        }
        else
        { 
            // 첫 5개와 마지막 5개만 출력
            DumpAll(documents.Take(5));
            AnsiConsole.MarkupLine("\r\n[grey bold]......[/]\r\n");
            DumpAll(documents.Skip(count - 5).Take(5));
        }
    }

    static void DumpAll(IEnumerable<DbProfileInfo> documents)
    {
        foreach (var doc in documents)
        {
            AnsiConsole.MarkupLine($"[green]{doc.Timestamp.ToLocalTime():yyyy-MM-dd HH:mm:ss.fff}[/] [blue]{doc.CommandText}[/] ({doc.ExecutionTime:N2}ms)");
            if (doc.ParameterInfos != null)
            {
                foreach (var pair in doc.ParameterInfos)
                {
                    AnsiConsole.MarkupLine($"  [grey]{pair.Key} = {pair.Value}[/]");
                }
            }
        }
    }
}

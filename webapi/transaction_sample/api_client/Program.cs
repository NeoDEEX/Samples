using NeoDEEX.ServiceModel.Data;
using NeoDEEX.ServiceModel.Client.Data;
using Spectre.Console;
using System.Data;
using System.Text;

namespace api_client;

internal class Program
{
    static async Task Main(string[] args)
    {
        AnsiConsole.MarkupLine("[green]Transaction Test using Fox Data Service Client ...[/]");
        AnsiConsole.WriteLine();

        bool useSqlServer = false;
        bool isLocalTx = true;
        bool forceRollback = true;

        if (await TestSetup(useSqlServer) == false)
        {
            return;
        }

        string db1 = "AzurePostgreSQL";
        string db2 = "OciPostgreSQL";
        if (useSqlServer == true)
        {
            db2 = "SqlServer";
        }
        if (isLocalTx)
        {
            db2 = db1;
        }
        await GetProducts(db1);
        await GetTestTable(db2);
        AnsiConsole.WriteLine();
        await UpdateData(db1, db2, forceRollback);
        AnsiConsole.WriteLine();
        await GetProducts(db1);
        await GetTestTable(db2);
        AnsiConsole.WriteLine();
    }

    static async Task<bool> TestSetup(bool useSqlServer = true)
    {
        AnsiConsole.Markup("[blue]Setting up test environment...[/]");
        FoxDataRequestCollection requests =
        [
            new("products.setup_test_data", "AzurePostgreSQL") { Operation = FoxDataOperations.ExecuteNonQuery },
            new("testtable.setup_test_data", "AzurePostgreSQL") { Operation = FoxDataOperations.ExecuteNonQuery },
            new("products.setup_test_data", "OciPostgreSQL") { Operation = FoxDataOperations.ExecuteNonQuery },
            new("testtable.setup_test_data", "OciPostgreSQL") { Operation = FoxDataOperations.ExecuteNonQuery },
        ];
        if (useSqlServer == true)
        {
            requests.Add(new("products.setup_test_data", "SqlServer") { Operation = FoxDataOperations.ExecuteNonQuery });
            requests.Add(new("testtable.setup_test_data", "SqlServer") { Operation = FoxDataOperations.ExecuteNonQuery });
        }
        FoxDataServiceClient client = new(String.Empty);
        try
        {
            Task[] tasks = client.ExecuteParallelAsync(requests);
            await Task.WhenAll(tasks);
            AnsiConsole.MarkupLine(" [blue]Done![/]");
            return true;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine(" [red]Error![/]");
            AnsiConsole.WriteLine(ex.Message);
            return false;
        }
    }

    static async Task GetProducts(string? databaseName = null)
    {
        string dbName = databaseName ?? "<default>";
        FoxDataRequest request = new("products.get_test_data", databaseName);
        FoxDataServiceClient client = new(String.Empty);
        FoxDataResponse response = await client.ExecuteDataSetAsync(request);
        DataTable dt = response.DataSet.Tables[0];
        AnsiConsole.MarkupLine($"[green]Products (db={dbName}), rows={dt.Rows.Count}[/]");
        if (dt.Rows.Count > 0)
        {
            var table = new Table();
            table.Border(TableBorder.AsciiDoubleHead);
            table.AddColumn("Product ID");
            table.AddColumn("Product Name");
            table.AddColumn("Unit Price");
            foreach (DataRow row in dt.Rows)
            {
                string productId = row.Field<string>("product_id") ?? "(null)";
                string productName = row.Field<string>("product_name") ?? "(null)";
                string unitPrice = row.Field<double>("unit_price").ToString();
                if (productId == "PROD999")
                {
                     // Highlight the test data inserted by transaction test
                    productId = $"[yellow]{productId}[/]";
                    productName = $"[yellow]{productName}[/]";
                    unitPrice = $"[yellow]{unitPrice}[/]";
                }
                table.AddRow(productId, productName, unitPrice);
            }
            AnsiConsole.Write(table);
        }
    }

    static async Task GetTestTable(string? databaseName = null)
    {
        string dbName = databaseName ?? "<default>";
        FoxDataRequest request = new("testtable.get_test_data", databaseName);
        FoxDataServiceClient client = new(String.Empty);
        FoxDataResponse response = await client.ExecuteDataSetAsync(request);
        DataTable dt = response.DataSet.Tables[0];
        AnsiConsole.MarkupLine($"[green]TxTestTable (db={dbName}), rows={dt.Rows.Count}[/]");
        if (dt.Rows.Count > 0)
        {
            var table = new Table();
            table.Border(TableBorder.AsciiDoubleHead);
            table.AddColumn("PK");
            table.AddColumn("COL1");
            table.AddColumn("COL2");
            foreach (DataRow row in dt.Rows)
            {
                string pk = row.Field<long>("PK").ToString();
                string col1 = row.Field<long>("COL1").ToString();
                string col2 = row.Field<string>("COL2") ?? "(null)";
                if (pk == "999")
                {
                    // Highlight the test data inserted by transaction test
                    pk = $"[yellow]{pk}[/]";
                    col1 = $"[yellow]{col1}[/]";
                    col2 = $"[yellow]{col2}[/]";
                }
                table.AddRow(pk, col1, col2);
            }
            AnsiConsole.Write(table);
        }
    }

    static async Task<bool> UpdateData(string db1, string db2, bool forceRollback = false)
    {
        FoxDataRequestCollection requests =
        [
            new("products.insert", db1)
            {
                Parameters = { { "product_name", "Tx test data" }, { "unit_price", 999.99 } },
                Operation = FoxDataOperations.ExecuteNonQuery
            },
            new("testtable.insert", db2)
            {
                Parameters = { { "col1", 999 }, { "col2", "Tx test data" } },
                Operation = FoxDataOperations.ExecuteNonQuery
            }
        ];
        // 두 개의 데이터베이스가 동일한 경우, 단일 로컬 트랜잭션으로 처리하도록 설정
        if (db1 == db2)
        {
            requests.DatabaseName = db1;
            requests[0].DatabaseName = null;
            requests[1].DatabaseName = null;
            requests.Transaction = FoxDataTransactions.Local;
        }
        else
        {
            requests.Transaction = FoxDataTransactions.Distributed;
        }
        // 트랜잭션 타임아웃 및 격리 수준 설정 (옵션)
        requests.TransactionTimeout = 30;
        requests.TransactionIsolation = FoxDataTransactionIsolations.Serializable;
        return await DoUpdate(requests, forceRollback);
    }

    static async Task<bool> DoUpdate(FoxDataRequestCollection requests, bool forceRollback)
    {
        string db1 = requests.DatabaseName ?? requests[0].DatabaseName ?? "<default>";
        string db2 = requests.DatabaseName ?? requests[1].DatabaseName ?? "<default>";
        if (forceRollback)
        {
            // 테스트를 위해 트랜잭션을 강제로 롤백하는 플래그 설정.
            requests.Diagnostics |= FoxDataRequestDiagnostics.ForceRollback;
        }

        StringBuilder sb = new(128);
        sb.Append("[blue]Transation Test Details: [/]")
            .Append("[gray]db1=[/]").Append("[magenta]").Append(db1).Append("[/]")
            .Append(", [gray]db2=[/]").Append("[magenta]").Append(db2).Append("[/]")
            .Append(", [gray]tx_kind=[/]").Append("[magenta]").Append(requests.Transaction).Append("[/]")
            .Append(", [gray]tx_result=[/]");
        if (forceRollback == true)
        {
            sb.Append("[red]").Append("rollback").Append("[/]");
        }
        else
        {
            sb.Append("[blue]").Append("commit").Append("[/]");
        }
        AnsiConsole.MarkupLine(sb.ToString());

        AnsiConsole.Markup("[blue]Updating database(s) using transaction...[/]");
        try
        {
            FoxDataServiceClient client = new(String.Empty);
            await client.ExecuteMultipleAsync(requests);
            AnsiConsole.MarkupLine(" [green]Done![/]");
            return true;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine(" [red]Error![/]");
            AnsiConsole.WriteLine(ex.ToString());
            return false;
        }
    }
}

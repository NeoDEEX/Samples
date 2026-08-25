using NeoDEEX.ServiceModel;
using NeoDEEX.ServiceModel.Data;
using Spectre.Console;
using System.Data;

namespace client_app;

internal static class Helper
{
    public static void Dump(this FoxServiceErrorInfo errorInfo)
    {
        AnsiConsole.MarkupLine($"[yellow]Message:[/] [red]{errorInfo.Message}[/]");
        AnsiConsole.MarkupLine($"[yellow]Detail:[/] [gray]{errorInfo.MessageDetail}[/]");
        AnsiConsole.MarkupLine($"[yellow]Exception Type:[/] [blue]{errorInfo.ExceptionType}[/]");
        AnsiConsole.MarkupLine($"[yellow]Error Code:[/] [blue]{errorInfo.ErrorCode}[/]");
        AnsiConsole.MarkupLine($"[yellow]Stack Trace:[/] [gray]{Markup.Escape(errorInfo.StackTrace)}[/]");
    }

    // Products 테이블의 내용을 표시한다.
    public static void DumpProducts(this FoxDataResponse response)
    {
        DataTable dt = response.DataSet.Tables[0];
        AnsiConsole.MarkupLine($"[green]Products rows={dt.Rows.Count}[/]");
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
                if (productName.StartsWith("Product") == false)
                {
                    // Highlight the test data inserted by transaction test
                    productId = $"[yellow][bold][underline]{productId}[/][/][/]";
                    productName = $"[yellow][bold][underline]{productName}[/][/][/]";
                    unitPrice = $"[yellow][bold][underline]{unitPrice}[/][/][/]";
                }
                table.AddRow(productId, productName, unitPrice);
            }
            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
        }
    }
}

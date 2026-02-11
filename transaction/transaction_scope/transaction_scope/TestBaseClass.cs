using NeoDEEX.Transactions;
using Spectre.Console;
using System.Runtime.CompilerServices;
using System.Text;
using System.Transactions;

namespace transaction_scope;

public class TestBaseClass : FoxComponentBase
{
    private const int indet_size = 2;

    protected static void DumpInfo(int indent, [CallerMemberName] string? methodName = null)
    {
        FoxTransactionOption txOption = FoxContextUtil.TransactionInfo.TransactionOption;
        bool isInTx = FoxContextUtil.IsInTransaction;
        Transaction? tx = Transaction.Current;
        StringBuilder sb = new();
        sb.Append(' ', indent * indet_size)
            .Append("[gray]")
            .Append("[bold darkcyan]").Append(methodName).Append("()[/] ")
            .Append("invoked... tx_option=")
            .Append("[bold underline green]").Append(txOption).Append("[/]");
        sb.Append(", is_in_tx=");
        if (isInTx == true)
        {
            sb.Append("[bold blue]O[/]");
        }
        else
        {
            sb.Append("[bold red]X[/]");
        }
        sb.Append(", tx_id=");
        if (isInTx == true)
        {
            sb.Append("[blue]");
        }
        else
        {
            sb.Append("[gray]");
        }
        sb.Append(ExtractId(tx?.TransactionInformation.LocalIdentifier)).Append("[/]").Append("[/]");
        AnsiConsole.MarkupLine(sb.ToString());
    }

    // 트랜잭션 ID 는 "guid:id" 형식이고 guid는 동일하므로 : 이후 만을 추출한다.
    protected static string? ExtractId(string? input)
    {
        if (input == null)
        {
            return "(null)";
        }
        int idx = input.IndexOf(':');
        if (idx < 0 || idx == input.Length - 1) return null;

        string result = input[(idx + 1)..];
        return result;
    }



    protected static TInterface CreateTxObject<T, TInterface>() 
        where T : FoxComponentBase, new()
        where TInterface : class
    {
        T obj = new();
        TInterface itf = obj.CreateExecution<TInterface>();
        return itf;
    }
}

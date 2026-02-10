using NeoDEEX.Data;
using NeoDEEX.Transactions;
using Spectre.Console;

namespace component_base_demo;

public class TxSubComp : FoxDacBase, ITxSubComp
{
    protected override void OnError(Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]>> OnError: exception in TxSubComp.{this.Context.MethodName} method: {ex.Message}[/]");
    }

    [FoxTransaction(FoxTransactionOption.Supported)]
    public void TxMethod(bool throwException = false)
    {
        AnsiConsole.MarkupLine("[white]TxSubComp.TxMethod invoked...[/]");
        this.DbAccess.DbProfileSettings.SaveDbProfileInfo = true;
        this.DbAccess.ExecuteSqlDataSet("SELECT * FROM TxTestTable");

        if (throwException)
        {
            throw new ApplicationException("Simulated exception in TxSubComp.TxMethod.");
        }
    }
}

public interface ITxSubComp : IDisposable
{
    void TxMethod(bool throwException = false);
}

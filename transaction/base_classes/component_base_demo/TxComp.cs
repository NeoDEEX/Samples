using NeoDEEX.Data;
using NeoDEEX.Transactions;
using NeoDEEX.Transactions.Common;
using Spectre.Console;
using System.Data;

namespace component_base_demo;

public class TxComp : FoxComponentBase, ITxComp
{
    public TxComp()
    {
        this.HandleChildException = false;
    }

    protected override void Activate()
    {
        base.Activate();
        FoxExecutionContext ctx = this.Context;
        AnsiConsole.MarkupLine($"[yellow]>> {ctx.MethodName} activated...[/]");
        FoxTransactionInfo? txInfo = ctx.TransactionInfo;
        if (txInfo != null)
        {
            AnsiConsole.MarkupLine($"[gray]>>  tx option: {txInfo.TransactionOption}[/]");
            AnsiConsole.MarkupLine($"[gray]>>  is in tx: {ctx.IsInTransaction.ToString().ToLower()}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[gray]>>  non-tx method[/]");
        }
    }

    protected override void Deactivate()
    {
        AnsiConsole.MarkupLine($"[yellow]>> {this.Context.MethodName} deactivated...[/]");
        if (this.Context.Exception != null)
        {
            AnsiConsole.MarkupLine($"[darkred]>>  method failed with exception: {this.Context.Exception.Message}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[darkgreen]>>  method completed successfully[/]");
        }
        base.Deactivate();
    }

    protected override void OnError(Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]>> OnError: exception in TxComp.{this.Context.MethodName} method: {ex.Message}[/]");
    }

    public void NonTxMethod()
    {
        AnsiConsole.MarkupLine("[white]TxComp.NonTxMethod invoked...[/]");
    }

    [FoxTransaction(FoxTransactionOption.Required)]
    public void TxMethod()
    {
        AnsiConsole.MarkupLine("[white]TxComp.TxMethod invoked...[/]");
        if (this.UserInfo != null)
        {
            AnsiConsole.MarkupLine($"[gray]>>  authenticated user id: {this.UserId}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[gray]>>  no user info available[/]");
        }
    }

    [FoxTransaction(FoxTransactionOption.None)]
    public void InvokeSub(bool throwException = false)
    {
        AnsiConsole.MarkupLine("[white]TxComp.InvokeSub invoked...[/]");

        TxSubComp subComp = new();
        ITxSubComp proxy = subComp.CreateExecution<ITxSubComp>();
        proxy.TxMethod(throwException);

        if (throwException)
        {
            throw new ApplicationException("Simulated exception in TxComp.InvokeSub.");
        }
    }

    [FoxTransaction(FoxTransactionOption.Required)]
    public DataSet GetData(bool throwException = false)
    {
        AnsiConsole.MarkupLine("[white]TxComp.GetData invoked...[/]");

        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        DataSet ds = dbAccess.ExecuteSqlDataSet("SELECT * FROM TxTestTable");

        if (throwException)
        {
            throw new ApplicationException("Simulated exception in TxMethod.");
        }

        return ds;
    }
}

public interface ITxComp : IDisposable
{
    void NonTxMethod();
    void TxMethod();
    void InvokeSub(bool throwException = false);
    DataSet GetData(bool throwException = false);
}

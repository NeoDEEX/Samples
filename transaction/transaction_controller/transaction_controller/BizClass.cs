using NeoDEEX.Transactions;
using NeoDEEX.Transactions.Common;
using Spectre.Console;
using System;

namespace transaction_controller;

public class BizClass : FoxBizBase, IBizClass
{
    public void Implicit()
    {
        this.DummpControllerInfo();
    }

    [FoxTransactionController(FoxTransactionControllerKind.Default)]
    public void Default()
    {
        this.DummpControllerInfo();
    }

    [FoxTransactionController(FoxTransactionControllerKind.RootContext)]
    public void RootContext()
    {
        // RootContext 값은 트랜잭션 컨트롤러 타입이 null 이다.
        this.DummpControllerInfo();
    }

    private void DummpControllerInfo()
    {
        AnsiConsole.MarkupLine($"[yellow]{this.Context.MethodName}() method transaction controller info:[/]");
        AnsiConsole.MarkupLine($"  [gray]controller: {this.Context.TransactionControllerType}[/]");
    }

    [FoxTransactionController(FoxTransactionControllerKind.FastTransaction)]
    public List<int> InsertMany_DistTx(int[] indexes, bool forceRollback = false)
    {
        return InsertMany(indexes, forceRollback);
    }

    [FoxTransactionController(FoxTransactionControllerKind.LocalTransaction)]
    public List<int> InsertMany_LocalTx(int[] indexes, bool forceRollback = false)
    {
        return InsertMany(indexes, forceRollback);
    }

    [FoxTransactionController(FoxTransactionControllerKind.Custom, CustomControllerType = typeof(CustomController))]
    public List<int> InsertMany_CustomTx(int[] indexes, bool forceRollback = false)
    {
        return InsertMany(indexes, forceRollback);
    }

    private List<int> InsertMany(int[] indexes, bool forceRollback)
    {
        this.DummpControllerInfo();

        using DacClass dac = new();
        IDacClass itf = dac.CreateExecution<IDacClass>();
        List<int> newIds = [];
        foreach (int index in indexes)
        {
            Memo memo = new()
            {
                Title = $"Memo #{index}",
                Content = $"Content #{index}",
            };
            int id = itf.InsertMemo(memo);
            newIds.Add(id);
        }
        if (forceRollback)
        {
            throw new ApplicationException("Force rollback");
        }
        return newIds;
    }

    [FoxTransactionController(FoxTransactionControllerKind.LocalTransaction, ThrowExceptionOnLocked = false)]
    public void DoBadDbAccess()
    {
        this.DummpControllerInfo();

        using DacClass dac = new();
        IDacClass itf = dac.CreateExecution<IDacClass>();
        itf.BadDbAccess();
    }
}

public interface IBizClass : IDisposable
{
    void Implicit();
    void Default();
    void RootContext();
    List<int> InsertMany_DistTx(int[] indexes, bool forceRollback = false);
    List<int> InsertMany_LocalTx(int[] indexes, bool forceRollback = false);
    List<int> InsertMany_CustomTx(int[] indexes, bool forceRollback = false);
    void DoBadDbAccess();
}

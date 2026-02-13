using NeoDEEX.Data;
using NeoDEEX.Transactions;
using Spectre.Console;

namespace transaction_controller;

public class DacClass : FoxDacBase, IDacClass
{
    public int InsertMemo(Memo memo)
    {
        AnsiConsole.MarkupLine($"[gray]  DbAccess instance id =[/][blue]{this.DbAccess.GetHashCode()}[/]");

        object? result = this.DbAccess.ExecuteQueryScalar("memo.insert", memo)
            ?? throw new InvalidOperationException("InsertMemo failed: result is null.");
        int newId = Convert.ToInt32(result);
        return newId;
    }

    public void BadDbAccess()
    {
        Memo memo_will_be_rollbacked = new()
        { 
            Title = "Good memo",
            Content = "This memo will be rollbacked"
        };
        this.DbAccess.ExecuteQueryNonQuery("memo.insert", memo_will_be_rollbacked);

        Memo memo_will_not_be_rollbacked = new()
        {
            Title = "Bad memo",
            Content = "This memo will NOT be rollbacked"
        };
        FoxDbAccess NonTransactionalDbAccess = FoxDbAccess.CreateDbAccess();
        NonTransactionalDbAccess.ExecuteQueryNonQuery("memo.insert", memo_will_not_be_rollbacked);

        throw new ApplicationException("Force rollback");
    }
}

public interface IDacClass : IDisposable
{
    int InsertMemo(Memo memo);
    void BadDbAccess();
}

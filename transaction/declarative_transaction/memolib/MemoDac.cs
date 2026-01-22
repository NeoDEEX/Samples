using NeoDEEX.Transactions;

namespace memolib;

[FoxTransaction(FoxTransactionOption.Supported)]
public class MemoDac : FoxDacBase, IMemoDac
{
    [FoxAutoComplete]
    public List<Memo> GetMemos()
    {
        List<Memo> list = this.DbAccess.ExecuteQueryList<Memo>("memo.getall");
        return list;
    }

    [FoxAutoComplete]
    public int InsertMemo(Memo memo)
    {
        object? result = this.DbAccess.ExecuteQueryScalar("memo.insert", memo) 
            ?? throw new InvalidOperationException("InsertMemo failed: result is null.");
        int newId = Convert.ToInt32(result);
        return newId;
    }
}

public interface IMemoDac
{
    List<Memo> GetMemos();
    int InsertMemo(Memo memo);
}

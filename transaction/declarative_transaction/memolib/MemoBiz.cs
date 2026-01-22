using NeoDEEX.Transactions;

namespace memolib;

[FoxTransaction(FoxTransactionOption.Required)]
public class MemoBiz : FoxBizBase, IMemoBiz
{
    [FoxAutoComplete]
    public int Insert(Memo memo)
    {
        using MemoDac dac = new();
        IMemoDac itf = dac.CreateExecution<IMemoDac>();
        int newId = itf.InsertMemo(memo);
        return newId;
    }

    [FoxAutoComplete]
    public List<int> InsertMany(Memo[] memos)
    {
        using MemoDac dac = new();
        IMemoDac itf = dac.CreateExecution<IMemoDac>();
        List<int> savedIds = [];
        foreach (var memo in memos)
        {
            int newId = itf.InsertMemo(memo);
            savedIds.Add(newId);
        }
        return savedIds;
    }
}

public interface IMemoBiz
{
    int Insert(Memo memo);
    List<int> InsertMany(Memo[] memos);
}

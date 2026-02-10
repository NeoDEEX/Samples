using NeoDEEX.Transactions;

namespace dac_base_demo;

//[FoxTransactionController(FoxTransactionControllerKind.LocalTransaction, ThrowExceptionOnLocked = false)]
[FoxTransactionController(FoxTransactionControllerKind.LocalTransaction)]
public class BizClass : FoxBizBase, IBizClass
{
    public List<int> InsertMany(IEnumerable<Memo> memos, bool forceRollback)
    {
        using DacClass dac = new();
        IDacClass itf = dac.CreateExecution<IDacClass>();
        List<int> newIds = [];
        foreach (Memo memo in memos)
        {
            int id = itf.InsertMemo(memo);
            if (forceRollback)
            {
                throw new ApplicationException("Force rollback");
            }
            newIds.Add(id);
        }
        return newIds;
    }

    public List<Memo> Insert(Memo memo)
    {
        using DacClass dac = new();
        IDacClass itf = dac.CreateExecution<IDacClass>();
        return itf.InsertAndQuery(memo);
    }
}

public interface IBizClass : IDisposable
{
    List<int> InsertMany(IEnumerable<Memo> memos, bool forceRollback);
    List<Memo> Insert(Memo memo);
}

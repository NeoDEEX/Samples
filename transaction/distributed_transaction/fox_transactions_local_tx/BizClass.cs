using NeoDEEX.Transactions;

namespace fox_transactions_local_tx;

[FoxTransaction(FoxTransactionOption.Required)]
[FoxTransactionController(FoxTransactionControllerKind.LocalTransaction)]
public class BizClass : FoxBizBase, IBizClass
{
    [FoxAutoComplete]
    public void InsertMany(int[] ids, bool commit)
    {
        using DacClass dac = new();
        IDacClass itf = dac.CreateExecution<IDacClass>();
        foreach (int id in ids)
        {
            itf.InsertData(id, id, "STR #" + id);
        }
        if (!commit)
        {
            throw new ApplicationException("Simulated error to rollback single DB transaction.");
        }
    }
}

public interface IBizClass
{
    void InsertMany(int[] ids, bool commit);
}

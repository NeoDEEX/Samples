using NeoDEEX.Transactions;

namespace fox_transactions_dist_tx;

[FoxTransaction(FoxTransactionOption.Required)]
public class BizClass : FoxBizBase, IBizClass
{
    [FoxAutoComplete]
    public void DoDistributedTransaction(bool commit)
    {
        using DacClass1 dac1 = new();
        using DacClass2 dac2 = new();
        IDacClass itf1 = dac1.CreateExecution<IDacClass>();
        IDacClass itf2 = dac2.CreateExecution<IDacClass>();
        itf1.InsertData();
        itf2.InsertData();
        if (!commit)
        {
            throw new ApplicationException("Simulated error to rollback distributed transaction.");
        }
    }

    [FoxAutoComplete]
    public void InsertMany(int[] ids,bool commit)
    {
        using DacClass1 dac1 = new();
        IDacClass itf1 = dac1.CreateExecution<IDacClass>();
        foreach (int id in ids)
        {
            itf1.InsertData(id, id, "STR #" + id);
        }
        if (!commit)
        {
            throw new ApplicationException("Simulated error to rollback single DB transaction.");
        }
    }
}

public interface IBizClass
{
    void DoDistributedTransaction(bool commit);
    void InsertMany(int[] ids,bool commit);
}

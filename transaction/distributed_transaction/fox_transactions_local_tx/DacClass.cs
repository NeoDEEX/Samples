using common;
using NeoDEEX.Data;
using NeoDEEX.Transactions;
using System.Diagnostics;

namespace fox_transactions_local_tx;

[FoxTransactionController(FoxTransactionControllerKind.RootContext)]
public class DacClass : FoxDacBase, IDacClass
{
    protected override FoxDbAccess CreateDbInstance()
    {
        return FoxDbAccess.CreateDbAccess(TestUtils.TestDB1);
    }

    public void InsertData(int id, int col1, string col2)
    {
        Debug.Write($"  IsOpen: {this.DbAccess.IsOpen}  IsInTx: {this.DbAccess.IsInLocalTransaction}");

        var parameters = this.DbAccess.CreateParamCollection();
        parameters.AddWithValue("pk", id);
        parameters.AddWithValue("col1", col1);
        parameters.AddWithValue("col2", col2);
        this.DbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(:PK, :COL1, :COL2)", parameters);
    }
}

public interface IDacClass : IDisposable
{
    void InsertData(int id, int col1, string col2);
}


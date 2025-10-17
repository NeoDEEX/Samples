using NeoDEEX.Data;
using NeoDEEX.Transactions;

namespace TransactionalComponent;

public class TxComp : FoxComponentBase, ITxComp
{
    [FoxTransaction(FoxTransactionOption.Required)]
    public void InsertData(int id)
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        string query = "INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(@id, 1, 'A')";
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        parameters.AddWithValue("id", id);
        dbAccess.ExecuteSqlNonQuery(query, parameters);
        //throw new InvalidOperationException("Rollback Test Exception");
    }
}

public interface ITxComp
{
    void InsertData(int id);
}
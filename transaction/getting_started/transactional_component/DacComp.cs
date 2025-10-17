using NeoDEEX.Data;
using NeoDEEX.Transactions;

namespace TransactionalComponent;

public interface IDacComp
{
    void InsertData(int id);
}

public class DacComp : FoxDacBase, IDacComp
{
    public void InsertData(int id)
    {
        string query = "INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(@id, 1, 'A')";
        FoxDbParameterCollection parameters = this.DbAccess.CreateParamCollection();
        parameters.AddWithValue("id", id);
        this.DbAccess.ExecuteSqlNonQuery(query, parameters);
    }
}

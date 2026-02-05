using NeoDEEX.Transactions;

namespace execution_context;

public class DacClass : FoxDacBase, IDacClass
{
    public void InsertData(int id, int col1, string col2)
    {
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


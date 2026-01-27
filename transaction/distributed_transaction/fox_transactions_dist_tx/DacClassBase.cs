using NeoDEEX.Transactions;

namespace fox_transactions_dist_tx;

public abstract class DacClassBase : FoxDacBase, IDacClass
{
    public void InsertData()
    {
        this.DbAccess.Open();
        this.DbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(998, 9, 'X')");
        this.DbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(999, 9, 'Y')");
        this.DbAccess.Close();
        // FoxDacBase 는 메서드 종료 시점에서 예외 발생과 무관하게
        // DbAccess 인스턴스를 자동으로 Close/Dispose 처리한다.
        // 따라서 별도의 try~catch~finally 블록이 필요 없다.
    }

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
    void InsertData();
    void InsertData(int id, int col1, string col2);
}


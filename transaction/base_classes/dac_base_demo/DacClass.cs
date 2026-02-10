using NeoDEEX.Data;
using NeoDEEX.Transactions;

namespace dac_base_demo;

public class DacClass(string? connName = null) : FoxDacBase, IDacClass
{
    public string? ConnectionStringName { get; set; } = connName;

    protected override FoxDbAccess CreateDbInstance()
    {
        return FoxDbAccess.CreateDbAccess(this.ConnectionStringName);
    }

    public int InsertMemo(Memo memo)
    {
        object? result = this.DbAccess.ExecuteQueryScalar("memo.insert", memo)
            ?? throw new InvalidOperationException("InsertMemo failed: result is null.");
        int newId = Convert.ToInt32(result);
        return newId;
    }

    public List<Memo> InsertAndQuery(Memo memo)
    {
        this.DbAccess.Open();
        this.DbAccess.ExecuteQueryNonQuery("memo.insert", memo);
        List<Memo> memos = this.DbAccess.ExecuteQueryList<Memo>("memo.getall");
        this.DbAccess.Close();
        return memos;
    }
}

public interface IDacClass : IDisposable
{
    int InsertMemo(Memo memo);
    List<Memo> InsertAndQuery(Memo memo);
}

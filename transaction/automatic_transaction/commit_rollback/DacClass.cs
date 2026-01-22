using NeoDEEX.Data;
using NeoDEEX.Transactions;

namespace commit_rollback;

public class DacClass : FoxDacBase, IDacClass
{
    [FoxAutoComplete]
    public void AutoCompleteInsertData(int id)
    {
        InsertData(id);
    }

    [FoxAutoComplete(false)]
    public void ManualCompleteInsertData(int id)
    {
        try
        {
            InsertData(id);
            FoxContextUtil.SetComplete();
        }
        catch
        {
            FoxContextUtil.SetAbort();
            throw;
        }
    }

    private void InsertData(int id)
    {
        string query = "INSERT INTO my_demo_table(col_id, col_str) VALUES(:id, :str)";
        FoxDbParameterCollection parameters = this.DbAccess.CreateParamCollection();
        parameters.AddWithValue("id", id);
        parameters.AddWithValue("str", "str #" + id);
        this.DbAccess.ExecuteSqlNonQuery(query, parameters);
    }
}

public interface IDacClass
{
    void AutoCompleteInsertData(int id);
}

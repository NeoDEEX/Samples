using NeoDEEX.Transactions;
using System.Data;

namespace BizLib1;

public class TxDataAccess : FoxDacBase, ITxDataAccess
{
    public DataSet GetTestData()
    {
        return this.DbAccess.ExecuteQueryDataSet("testtable.get_test_data");
    }

    // 트랜잭션 하에서 테스트 데이터를 추가하는 메서드
    public int InsertTestData(string data)
    {
        var parameters = new
        {
            col1 = 9,
            col2 = data
        };
        return this.DbAccess.ExecuteQueryNonQuery("testtable.insert", parameters);
    }

    // 테스트 데이터를 초기화 하는 메서드
    public void InitTestData()
    {
        this.DbAccess.ExecuteQueryNonQuery("testtable.setup_test_data");
    }
}

public interface ITxDataAccess : IDisposable
{
    DataSet GetTestData();
    int InsertTestData(string data);
    void InitTestData();
}

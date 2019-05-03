using System.Data;
using TheOne.Transactions;

namespace BizLib
{
    // 데이터 액세스 컴포넌트 예제
    [FoxTransaction(FoxTransactionOption.Supported)]
    class DataAccessClass : FoxDacBase
    {
        public DataSet GetAllProducts()
        {
            // FoxDbAccess 를 사용하여 데이터를 조회하여 반환한다.
            return this.DbAccess.ExecuteSqlDataSet("SELECT * FROM Products");
        }
    }
}

using System.Data;
using TheOne.Transactions;

namespace BizLib
{
    // 비즈니스 로직 컴포넌트 예제
    public class BizLogicClass : FoxBizBase
    {
        [FoxTransaction(FoxTransactionOption.Suppress)]
        public DataSet GetAllProducts()
        {
            using (var dac = new DataAccessClass())
            {
                // 데이터 액세스 컴포넌트를 호출한다.
                return dac.GetAllProducts();
            }
        }
    }
}

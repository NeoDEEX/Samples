using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOne.Transactions;

namespace BizLib
{
    public class BizLogicClass : FoxBizBase
    {
        [FoxTransaction(FoxTransactionOption.Suppress)]
        public DataSet GetAllProducts()
        {
            using (var dac = new DataAccessClass())
            {
                return dac.GetAllProducts();
            }
        }
    }
}

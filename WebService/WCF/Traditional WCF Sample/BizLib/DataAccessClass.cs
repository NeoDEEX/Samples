using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOne.Transactions;

namespace BizLib
{
    [FoxTransaction(FoxTransactionOption.Supported)]
    class DataAccessClass : FoxDacBase
    {
        public DataSet GetAllProducts()
        {
            return this.DbAccess.ExecuteSqlDataSet("SELECT * FROM Products");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMacro
{
    internal class DacComp : DacBase
    {
        public DataSet GetData(int? categoryId)
        {
            var parameters = new { CategoryId = categoryId };
            return this.DbAccess.ExecuteQueryDataSet("Query.GetData", parameters);
        }
    }
}

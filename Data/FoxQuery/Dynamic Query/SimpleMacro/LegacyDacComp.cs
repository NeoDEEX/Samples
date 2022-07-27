using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMacro
{
    internal class LegacyDacComp : DacBase
    {
        public DataSet GetData(int? categoryId)
        {
            if (categoryId == null)
            {
                return this.DbAccess.ExecuteQueryDataSet("LegacyQuery.GetAll");
            }
            else
            {
                var parameters = new { CategoryId = categoryId };
                return this.DbAccess.ExecuteQueryDataSet("LegacyQuery.GetByCategory", parameters);
            }
        }
    }
}

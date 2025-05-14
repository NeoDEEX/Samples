using NeoDEEX.Data;
using NeoDEEX.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace northwind_biz;

public class NorthwindDac : FoxDacBase, INorthwindDac
{
    public DataSet GetProductsByName(string? product_name)
    {
        string sql = "SELECT * FROM Products ";
        FoxDbParameterCollection parameters = this.DbAccess.CreateParamCollection();
        if (String.IsNullOrEmpty(product_name) == false)
        {
            product_name = "%" + product_name + "%";
            sql +=  "WHERE product_name LIKE :product_name";
            parameters.AddWithValue("product_name", product_name);
        }
        return this.DbAccess.ExecuteSqlDataSet(sql, parameters);
    }
}

public interface INorthwindDac
{
    DataSet GetProductsByName(string? product_name);
}

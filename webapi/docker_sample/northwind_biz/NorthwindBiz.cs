using NeoDEEX.ServiceModel.Services.Biz;
using NeoDEEX.Transactions;
using System.Data;

namespace northwind_biz;

[FoxBizClass("northwind")]
public class NorthwindBiz : FoxBizBase
{
    [FoxBizMethod("get_products_by_name")]
    [FoxTransaction(TransactionOption = FoxTransactionOption.Supported)]
    public DataSet GetProductsByName(string? product_name)
    {
        using NorthwindDac dac = new();
        INorthwindDac itf = dac.CreateExecution<INorthwindDac>();
        DataSet ds = itf.GetProductsByName(product_name);
        return ds;
    }
}

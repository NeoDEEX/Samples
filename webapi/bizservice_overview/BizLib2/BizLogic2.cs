using NeoDEEX.Data;
using NeoDEEX.ServiceModel.Services.Biz;
using System.Data;

namespace BizLib2;

#pragma warning disable CA1822 // Mark members as static

[FoxBizClass]
public class BizLogic2
{
    [FoxBizMethod]
    public DataSet SearchProduct(string name)
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new { keyword = name + '%' };
        DataSet ds = dbAccess.ExecuteQueryDataSet("northwind.search_by_name", parameters);
        return ds;
    }

    [FoxBizMethod]
    public Product? GetProduct(int id)
    {
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        var parameters = new { product_id = id };
        List<Product> list = dbAccess.ExecuteQueryList<Product>("northwind.get_product", parameters);
        if (list.Count > 0)
        {
            return list[0];
        }
        return null;
    }
}

public class Product
{
    public int Product_Id { get; set; }
    public string? Product_Name { get; set; }
    public int Category_Id { get; set; }
}
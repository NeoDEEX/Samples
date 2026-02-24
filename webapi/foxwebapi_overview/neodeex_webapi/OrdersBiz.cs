using commlib;
using NeoDEEX.ServiceModel.Services.Biz;
using NeoDEEX.Transactions;

namespace neodeex_webapi;

[FoxBizClass("OrdersBiz")]
public class OrdersBiz : FoxBizBase, IOrdersBiz
{
    [FoxBizMethod]
    public int InsertOrder(Order order, OrderDetail[]? details)
    {
        using OrdersDac dac = new();
        IOrdersDac itf = dac.CreateExecution<IOrdersDac>();
        int orderId = itf.InsertOrder(order);
        if (details != null)
        {
            foreach (var detail in details)
            {
                itf.InsertDetails(detail);
            }
        }
        return orderId;
    }
}

public interface IOrdersBiz : IDisposable
{
    int InsertOrder(Order order, OrderDetail[]? details);
}

using System.Transactions;
using commlib;

namespace legacy_webapi;

public class OrdersBiz : IOrdersBiz
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public List<Order> GetAllOrders()
    {
        using OrdersDac dac = new();
        return dac.GetAllOrders();
    }

    public Order? GetOrderById(int id)
    {
        using OrdersDac dac = new();
        return dac.GetOrderById(id);
    }

    public List<OrderDetail> GetDetailsByOrderId(int order_Id)
    {
        using OrdersDac dac = new();
        return dac.GetDetailsByOrderId(order_Id);
    }

    public int InsertOrder(Order order, OrderDetail[]? details)
    {
        using TransactionScope scope = new(TransactionScopeOption.Required);
        using OrdersDac dac = new();
        int orderId = dac.InsertOrder(order);
        if (details != null)
        {
            foreach (var detail in details)
            {
                detail.Order_Id = orderId;
                dac.InsertDetails(detail);
            }
        }
        scope.Complete();
        return orderId;
    }
}

public interface IOrdersBiz : IDisposable
{
    List<Order> GetAllOrders();
    Order? GetOrderById(int id);
    List<OrderDetail> GetDetailsByOrderId(int order_Id);
    int InsertOrder(Order order, OrderDetail[]? details);
}

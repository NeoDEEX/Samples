using NeoDEEX.Data;
using commlib;

namespace legacy_webapi;

public class OrdersDac : IOrdersDac
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public List<Order> GetAllOrders()
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        return dbAccess.GetAllOrders();
    }

    public Order? GetOrderById(int id)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        return dbAccess.GetOrderById(id);
    }

    public List<OrderDetail> GetDetailsByOrderId(int order_Id)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        return dbAccess.GetDetailsByOrderId(order_Id);
    }

    public int InsertOrder(Order order)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        return dbAccess.InsertOrder(order);
    }

    public void InsertDetails(OrderDetail detail)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        dbAccess.InsertDetails(detail);
    }
}

public interface IOrdersDac : IDisposable
{
    List<Order> GetAllOrders();
    Order? GetOrderById(int id);
    List<OrderDetail> GetDetailsByOrderId(int order_Id);
    int InsertOrder(Order order);
    void InsertDetails(OrderDetail detail);
}

using commlib;
using NeoDEEX.Transactions;

namespace neodeex_webapi;

public class OrdersDac : FoxDacBase, IOrdersDac
{
    public int InsertOrder(Order order)
    {
        return this.DbAccess.InsertOrder(order);
    }

    public void InsertDetails(OrderDetail detail)
    {
        this.DbAccess.InsertDetails(detail);
    }
}

public interface IOrdersDac : IDisposable
{
    int InsertOrder(Order order);
    void InsertDetails(OrderDetail detail);
}

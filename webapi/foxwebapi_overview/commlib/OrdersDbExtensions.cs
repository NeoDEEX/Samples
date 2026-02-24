using NeoDEEX.Data;

namespace commlib;

public static class OrdersDbExtensions
{
    public static void DeleteTestData(this FoxDbAccess dbAccess)
    {
        dbAccess.ExecuteSqlNonQuery("DELETE FROM t_order_details WHERE order_id > 5");
        dbAccess.ExecuteSqlNonQuery("DELETE FROM t_orders WHERE order_id > 5");
    }

    public static List<Order> GetAllOrders(this FoxDbAccess dbAccess)
    {
        //string query = "SELECT * FROM t_orders";
        //List<Order> orders = dbAccess.ExecuteSqlList<Order>(query);
        //return orders;
        return dbAccess.ExecuteQueryList<Order>("orders.get_all_orders");
    }

    public static Order? GetOrderById(this FoxDbAccess dbAccess, int id)
    {
        //string query = "SELECT * FROM t_orders WHERE order_id = @order_id";
        //var parameters = dbAccess.CreateParamCollection();
        //parameters.AddWithValue("order_id", id);
        //List<Order> orders = dbAccess.ExecuteSqlList<Order>(query, parameters);
        List<Order> orders = dbAccess.ExecuteQueryList<Order>("orders.get_order_by_id", new { order_id = id });
        if (orders.Count == 0)
        {
            return null;
        }
        return orders[0];
    }

    public static List<OrderDetail> GetDetailsByOrderId(this FoxDbAccess dbAccess, int order_Id)
    {
        //string query = "SELECT * FROM t_order_details WHERE order_id = @order_id";
        //var parameters = dbAccess.CreateParamCollection();
        //parameters.AddWithValue("order_id", order_Id);
        //List<OrderDetail> orderDetails = dbAccess.ExecuteSqlList<OrderDetail>(query, parameters);
        //return orderDetails;
        return dbAccess.ExecuteQueryList<OrderDetail>("orders.get_details_by_id", new { order_id = order_Id });
    }

    public static int InsertOrder(this FoxDbAccess dbAccess, Order order)
    {
        //string query =
        //    "INSERT INTO t_orders (customer_id, employee_id, order_date, shipped_date, ship_address) " +
        //    "VALUES (:customer_id, :employee_id, :order_date, :shipped_date, :ship_address) " +
        //    "RETURNING order_id";
        //var parameters = dbAccess.CreateParamCollection();
        //parameters.AddWithValue("customer_id", order.Customer_Id);
        //parameters.AddWithValue("employee_id", order.Employee_Id);
        //parameters.AddWithValue("order_date", order.Order_Date);
        //parameters.AddWithValue("shipped_date", order.Shipped_Date);
        //parameters.AddWithValue("ship_address", order.Ship_Address);
        //object? result = dbAccess.ExecuteSqlScalar(query, parameters);
        object? result = dbAccess.ExecuteQueryScalar("orders.insert_order", order);
        return Convert.ToInt32(result);
    }

    public static void InsertDetails(this FoxDbAccess dbAccess, OrderDetail detail)
    {
        //string query =
        //    "INSERT INTO t_order_details(order_id, product_id, quantity, unit_price) " +
        //    "VALUES(:order_id, :product_id, :quantity, :unit_price)";
        //var parameters = dbAccess.CreateParamCollection();
        //parameters.AddWithValue("order_id", detail.Order_Id);
        //parameters.AddWithValue("product_id", detail.Product_Id);
        //parameters.AddWithValue("quantity", detail.Quantity);
        //parameters.AddWithValue("unit_price", detail.Unit_Price);
        //dbAccess.ExecuteSqlNonQuery(query, parameters);
        dbAccess.ExecuteQueryNonQuery("orders.insert_detail", detail);
    }
}

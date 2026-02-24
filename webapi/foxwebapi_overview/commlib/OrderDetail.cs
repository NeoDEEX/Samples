namespace commlib;

public class OrderDetail
{
    public int Order_Id { get; set; }
    public string Product_Id { get; set; } = String.Empty;
    public int Quantity { get; set; }
    public decimal Unit_Price { get; set; }
}

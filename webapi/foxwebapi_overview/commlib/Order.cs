namespace commlib;

public class Order
{
    public int Order_Id { get; set; }
    public string Customer_Id { get; set; } = String.Empty;
    public string? Employee_Id { get; set; }
    public DateTime? Order_Date { get; set; }
    public DateTime? Shipped_Date { get; set; }
    public string? Ship_Address { get; set; }
}

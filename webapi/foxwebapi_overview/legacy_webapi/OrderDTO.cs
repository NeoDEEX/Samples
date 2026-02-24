using commlib;

namespace legacy_webapi;

public class OrderDTO
{
    public Order? Order { get; set; }
    public OrderDetail[]? Details { get; set; }
}

using commlib;
using Microsoft.AspNetCore.Mvc;

namespace legacy_webapi.Controllers;

[Route("api/orders")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders()
    {
        using OrdersBiz biz = new();
        List<Order> orders = biz.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public IActionResult GetOrderById(int id)
    {
        using OrdersBiz biz = new();
        Order? order = biz.GetOrderById(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpGet("{id}/details")]
    public IActionResult GetDetailsById(int id)
    {
        using OrdersBiz biz = new();
        Order? order = biz.GetOrderById(id);
        if (order == null)
        {
            return NotFound();
        }
        List<OrderDetail>? details = biz.GetDetailsByOrderId(id);
        return Ok(details);
    }

    [HttpPost("new")]
    public IActionResult CreateOrder([FromBody] OrderDTO? dto_order)
    {
        if (dto_order == null || dto_order.Order == null)
        {
            return BadRequest();
        }
        Order order = dto_order.Order;
        OrderDetail[]? details = dto_order.Details;
        using OrdersBiz biz = new();
        int orderId = biz.InsertOrder(order, details);
        return Ok(new { Order_Id = orderId });
    }
}

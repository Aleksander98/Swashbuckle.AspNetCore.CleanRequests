using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.CleanRequests.Examples.Models;

namespace Swashbuckle.AspNetCore.CleanRequests.Examples.Controllers;

[ApiController]
[Route("orders")]
[AllowAnonymous]
public class OrderController : ControllerBase
{
    [HttpGet("{orderId:int}/items")]
    public ActionResult<GetOrderItemsResponse> GetOrderItems(int orderId, 
        [FromQuery] GetOrderItemsRequest request)
    {
        request.OrderId = orderId;

        // ...

        return Ok(new GetOrderItemsResponse());
    }

    [HttpPost("{orderId:int}/items")]
    public ActionResult<int> CreateOrderItem(int orderId,
        [FromBody] CreateOrderItemRequest request)
    {
        request.OrderId = orderId;

        // ...

        return Ok(1);
    }

    [HttpPut("{orderId:int}/items/{itemId:int}")]
    public ActionResult<int> UpdateOrderItem(int orderId, int itemId,
        [FromBody] UpdateOrderItemRequest request)
    {
        request.OrderId = orderId;
        request.ItemId = itemId;

        // ...

        return Ok(1);
    }
}
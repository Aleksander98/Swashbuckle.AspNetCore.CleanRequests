namespace Swashbuckle.AspNetCore.CleanRequests.Examples.Models;

public class GetOrderItemsRequest
{
    public int OrderId { get; set; }

    public bool? Available { get; set; }

    public string? Name { get; set; }
}
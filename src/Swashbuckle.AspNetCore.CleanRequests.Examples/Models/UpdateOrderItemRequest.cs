namespace Swashbuckle.AspNetCore.CleanRequests.Examples.Models;

public class UpdateOrderItemRequest
{
    public int OrderId { get; set; }

    public int ItemId { get; set; }

    public string? Name { get; set; }

    public bool Available { get; set; }
}
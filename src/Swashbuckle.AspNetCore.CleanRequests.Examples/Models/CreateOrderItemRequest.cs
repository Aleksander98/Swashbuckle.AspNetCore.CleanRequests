namespace Swashbuckle.AspNetCore.CleanRequests.Examples.Models;

public class CreateOrderItemRequest
{
    public int OrderId { get; set; }
    
    public string? Name { get; set; }

    public bool Available { get; set; }
}
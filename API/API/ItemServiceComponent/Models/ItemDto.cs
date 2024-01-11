namespace API.ItemServiceComponent.Models;

public class ItemDto
{
    public Guid DiscountId { get; set; }
    public String? Name { get; set; }
    public String? Description { get; set; }
    public decimal Price { get; set; }
}
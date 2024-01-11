namespace API.OrdersComponent.Models;

public class OrderItemDto
{
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }
    public Guid TaxId { get; set; }
    public OrderItemType Type { get; set; }
    public Decimal Quantity { get; set; }
}
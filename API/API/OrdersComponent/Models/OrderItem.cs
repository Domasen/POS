namespace API.OrdersComponent.Models;

public enum OrderItemType
{
    Item,
    Service,
    Appointment
}

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }
    public Decimal UnitPrice { get; set; }
    public OrderItemType Type { get; set; }
    public Decimal Quantity { get; set; }
    public Decimal Subtotal { get; set; }
}
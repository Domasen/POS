namespace API.OrdersComponent.Models;

public class OrderDto
{
    public Guid CustomerId { get; set; }
    public Guid StaffId { get; set; }
    public DateTime Date { get; set; }
    public Decimal Tip { get; set; }
}
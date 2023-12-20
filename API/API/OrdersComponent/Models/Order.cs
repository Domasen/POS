namespace API.OrdersComponent.Models;

public enum Status
{
    Paid,
    Unpaid
}

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid StaffId { get; set; }
    public Status Status{ get; set; }
    public DateTime Date { get; set; }
    public Decimal TotalAmount { get; set; }
    public Decimal Tip { get; set; }
}
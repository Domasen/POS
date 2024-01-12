using System.ComponentModel.DataAnnotations.Schema;

namespace API.OrdersComponent.Models;
public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid StaffId { get; set; }
    public Status Status{ get; set; }
    public DateTime Date { get; set; }
    
    [NotMapped]
    public Decimal TotalAmount { get; set; }
    public Decimal Tip { get; set; }
}
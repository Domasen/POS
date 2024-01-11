namespace API.PaymentComponent.Models;

public class PaymentDto
{
    public Guid OrderId { get; set; }
    public Guid PaymentMethodId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}
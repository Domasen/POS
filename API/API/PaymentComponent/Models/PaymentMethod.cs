namespace API.PaymentComponent.Models;

public class PaymentMethod
{
    public Guid Id { get; set; }
    public string MethodName { get; set; } = String.Empty;
    public string MethodDescription { get; set; } = String.Empty;
  
}
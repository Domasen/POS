namespace API.OrdersComponent.Models;

public class Receipt
{
    public Guid OrderId { get; set; }
    public List<ReceiptItem>? ReceiptItems { get; set; }
    public Decimal Tip { get; set; }
    public Decimal Total { get; set; }
}
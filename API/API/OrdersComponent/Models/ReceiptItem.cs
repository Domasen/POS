namespace API.OrdersComponent.Models;

public class ReceiptItem
{
    public string? ItemName { get; set; }
    public Decimal UnitPrice { get; set; }
    public Decimal DiscountAmountPerUnit { get; set; }
    public Decimal TaxAmount { get; set; }
    public Decimal Quantity { get; set; }
    public Decimal Subtotal { get; set; }
}
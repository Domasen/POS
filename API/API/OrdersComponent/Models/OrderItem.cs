﻿namespace API.OrdersComponent.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }
    public Guid TaxId { get; set; }
    public Decimal UnitPrice { get; set; }
    public Decimal DiscountAmountPerUnit { get; set; }
    public OrderItemType Type { get; set; }
    public Decimal Quantity { get; set; }
    public Decimal Subtotal { get; set; }
    public Decimal TaxAmount { get; set; }
}
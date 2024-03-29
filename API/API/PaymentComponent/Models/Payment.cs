﻿namespace API.PaymentComponent.Models;

public class Payment
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid PaymentMethodId { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public decimal Change { get; set; }
    public DateTime PaymentDate { get; set; }

}
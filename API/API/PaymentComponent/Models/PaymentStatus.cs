namespace API.PaymentComponent.Models;

public enum PaymentStatus
{
    Pending,
    Complete,
    Refunded,
    Failed, 
    Abandoned, 
    Revoked,
    Cancelled
}
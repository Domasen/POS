namespace API.DiscountLoyaltyComponent.Models;

public class Discount
{
    public Guid Id { get; set; }
    public String? DiscountName { get; set; } 
    public decimal DiscountPercentage { get; set; }
    public DateTime ValidUntil { get; set; }
}
namespace API.DiscountLoyaltyComponent.Models;

public class DiscountDto
{
    public String? DiscountName { get; set; } 
    public decimal DiscountPercentage { get; set; }
    public DateTime ValidUntil { get; set; }
}
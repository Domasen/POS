namespace API.DiscountLoyaltyComponent.Models;

public class LoyaltyProgram
{
    public Guid Id { get; set; }
    public Guid BusinessId { get; set; }
    public int PointsPerPurchase { get; set; }
    public String? RedemptionRules { get; set; } 
    public String? SpecialBenefits { get; set; } 
    
}
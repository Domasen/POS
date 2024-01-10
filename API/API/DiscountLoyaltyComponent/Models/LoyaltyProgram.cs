namespace API.DiscountLoyaltyComponent.Models;

public class LoyaltyProgram
{
    public Guid Id { get; set; }
    public decimal PointsPerAmount { get; set; }
    public String? RedemptionRules { get; set; } 
    public String? SpecialBenefits { get; set; } 
    
}
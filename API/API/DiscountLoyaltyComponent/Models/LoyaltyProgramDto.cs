namespace API.DiscountLoyaltyComponent.Models;

public class LoyaltyProgramDto
{
    public decimal PointsPerAmount { get; set; }
    public String? RedemptionRules { get; set; } 
    public String? SpecialBenefits { get; set; } 
}
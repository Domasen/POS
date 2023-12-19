using API.DiscountLoyaltyComponent.Models;

namespace API.DiscountLoyaltyComponent.Repository;

public interface ILoyaltyProgramRepository
{
    Task<LoyaltyProgram> AddLoyaltyProgram(LoyaltyProgram loyaltyProgram);
    Task<LoyaltyProgram?> DeleteLoyaltyProgram(Guid loyaltyProgramId);
    Task<LoyaltyProgram?> GetLoyaltyProgram(Guid loyaltyProgramId);
    Task<IEnumerable<LoyaltyProgram>> GetLoyaltyProgram();
    Task<LoyaltyProgram?> UpdateLoyaltyProgram(LoyaltyProgram loyaltyProgram);
}
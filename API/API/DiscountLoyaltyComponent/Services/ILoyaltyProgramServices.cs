using API.DiscountLoyaltyComponent.Models;

namespace API.DiscountLoyaltyComponent.Services;

public interface ILoyaltyProgramServices
{
    Task<LoyaltyProgram> AddLoyaltyProgram(LoyaltyProgram loyaltyProgram);
    Task<LoyaltyProgram?> DeleteLoyaltyProgram(Guid loyaltyProgramId);
    Task<LoyaltyProgram?> GetLoyaltyProgram(Guid loyaltyProgramId);
    Task<IEnumerable<LoyaltyProgram>> GetLoyaltyPrograms();
    Task<LoyaltyProgram?> UpdateLoyaltyProgram(LoyaltyProgram loyaltyProgram);
}
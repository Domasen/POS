using API.DiscountLoyaltyComponent.Models;
using API.DiscountLoyaltyComponent.Repository;
using API.UsersComponent.Models;
using API.UsersComponent.Services;

namespace API.DiscountLoyaltyComponent.Services;

public class LoyaltyProgramServices : ILoyaltyProgramServices
{
    private readonly ILoyaltyProgramRepository _loyaltyProgramRepository;
    public LoyaltyProgramServices(ILoyaltyProgramRepository loyaltyProgramRepository)
    {
        _loyaltyProgramRepository = loyaltyProgramRepository;
    }
    
    public async Task<LoyaltyProgram> AddLoyaltyProgram(LoyaltyProgramDto loyaltyProgram)
    {
        LoyaltyProgram loyaltyProgramCreated = new LoyaltyProgram()
        {
            PointsPerAmount = loyaltyProgram.PointsPerAmount,
            RedemptionRules = loyaltyProgram.RedemptionRules,
            SpecialBenefits = loyaltyProgram.SpecialBenefits
        };
        return await _loyaltyProgramRepository.AddLoyaltyProgram(loyaltyProgramCreated);
    }

    public async Task<LoyaltyProgram?> DeleteLoyaltyProgram(Guid loyaltyProgramId)
    {
        return await _loyaltyProgramRepository.DeleteLoyaltyProgram(loyaltyProgramId);
    }

    public async Task<LoyaltyProgram?> GetLoyaltyProgram(Guid loyaltyProgramId)
    {
        return await _loyaltyProgramRepository.GetLoyaltyProgram(loyaltyProgramId);
    }

    public async Task<IEnumerable<LoyaltyProgram>> GetLoyaltyPrograms()
    {
        return await _loyaltyProgramRepository.GetLoyaltyPrograms();
    }

    public async Task<LoyaltyProgram?> UpdateLoyaltyProgram(LoyaltyProgram loyaltyProgram)
    {
        return await _loyaltyProgramRepository.UpdateLoyaltyProgram(loyaltyProgram);
    }
}
using API.DiscountLoyaltyComponent.Models;
using API.DiscountLoyaltyComponent.Repository;

namespace API.DiscountLoyaltyComponent.Services;

public class LoyaltyProgramServices : ILoyaltyProgramServices
{
    private readonly ILoyaltyProgramRepository _loyaltyProgramRepository;
    
    public LoyaltyProgramServices(ILoyaltyProgramRepository loyaltyProgramRepository)
    {
        _loyaltyProgramRepository = loyaltyProgramRepository;
    }
    
    public async Task<LoyaltyProgram> AddLoyaltyProgram(LoyaltyProgram loyaltyProgram)
    {
        return await _loyaltyProgramRepository.AddLoyaltyProgram(loyaltyProgram);
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
using API.Data;
using API.DiscountLoyaltyComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DiscountLoyaltyComponent.Repository;

public class LoyaltyProgramRepository : ILoyaltyProgramRepository
{
    private readonly DataContext _context;
    
    public LoyaltyProgramRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<LoyaltyProgram> AddLoyaltyProgram(LoyaltyProgram loyaltyProgram)
    {
        loyaltyProgram.Id = Guid.NewGuid();
        var result = await _context.LoyaltyPrograms.AddAsync(loyaltyProgram);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<LoyaltyProgram?> DeleteLoyaltyProgram(Guid loyaltyProgramId)
    {
        var result = await _context.LoyaltyPrograms.FirstOrDefaultAsync(l => l.Id == loyaltyProgramId);

        if (result != null)
        {
            _context.LoyaltyPrograms.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }
    
    public async Task<LoyaltyProgram?> GetLoyaltyProgram(Guid loyaltyProgramId)
    {
        return await _context.LoyaltyPrograms.FirstOrDefaultAsync(l => l.Id == loyaltyProgramId);
    }

    public async Task<IEnumerable<LoyaltyProgram>> GetLoyaltyPrograms()
    {
        return await _context.LoyaltyPrograms.ToListAsync();
    }
    
    
    
    public async Task<LoyaltyProgram?> UpdateLoyaltyProgram(LoyaltyProgram loyaltyProgram)
    {
        var result = await _context.LoyaltyPrograms.FirstOrDefaultAsync(l => l.Id == loyaltyProgram.Id);

        if (result != null)
        {
            result.Id = loyaltyProgram.Id;
            result.PointsPerPurchase = loyaltyProgram.PointsPerPurchase;
            result.RedemptionRules = loyaltyProgram.RedemptionRules;
            result.SpecialBenefits = loyaltyProgram.SpecialBenefits;
            
            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}
using API.Data;
using API.DiscountLoyaltyComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DiscountLoyaltyComponent.Repository;

public class DiscountRepository : IDiscountRepository
{
    private readonly DataContext _context;
    
    public DiscountRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Discount> AddDiscount(Discount discount)
    {
        discount.Id = Guid.NewGuid();
        var result = await _context.Discounts.AddAsync(discount);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<Discount?> DeleteDiscount(Guid discountId)
    {
        var result = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == discountId);

        if (result != null)
        {
            _context.Discounts.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }
    
    public async Task<Discount?> GetDiscount(Guid discountId)
    {
        return await _context.Discounts.FirstOrDefaultAsync(d => d.Id == discountId);
    }

    public async Task<IEnumerable<Discount>> GetDiscounts()
    {
        return await _context.Discounts.ToListAsync();
    }
    
    
    
    public async Task<Discount?> UpdateDiscount(Discount discount)
    {
        var result = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == discount.Id);

        if (result != null)
        {
            result.Id = discount.Id;
            result.DiscountName = discount.DiscountName;
            result.DiscountPercentage = discount.DiscountPercentage;
            result.ValidUntil = discount.ValidUntil;

            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}
using API.DiscountLoyaltyComponent.Models;
using API.DiscountLoyaltyComponent.Repository;

namespace API.DiscountLoyaltyComponent.Services;

public class DiscountServices : IDiscountServices
{
    private readonly IDiscountRepository _discountRepository;
    
    public DiscountServices(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }
    public async Task<Discount> AddDiscount(Discount discount)
    {
        return await _discountRepository.AddDiscount(discount);
    }

    public async Task<Discount?> DeleteDiscount(Guid discountId)
    {
        return await _discountRepository.DeleteDiscount(discountId);
    }

    public async Task<Discount?> GetDiscount(Guid discountId)
    {
        return await _discountRepository.GetDiscount(discountId);
    }

    public async Task<IEnumerable<Discount>> GetDiscounts()
    {
        return await _discountRepository.GetDiscounts();
    }

    public async Task<Discount?> UpdateDiscount(Discount discount)
    {
        return await _discountRepository.UpdateDiscount(discount);
    }
}
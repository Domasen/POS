using API.DiscountLoyaltyComponent.Models;

namespace API.DiscountLoyaltyComponent.Services;

public interface IDiscountServices
{
    Task<Discount> AddDiscount(Discount discount);
    Task<Discount?> DeleteDiscount(Guid discountId);
    Task<Discount?> GetDiscount(Guid discountId);
    Task<IEnumerable<Discount>> GetDiscounts();
    Task<Discount?> UpdateDiscount(Discount discount);
}
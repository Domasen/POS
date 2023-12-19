using API.DiscountLoyaltyComponent.Models;

namespace API.DiscountLoyaltyComponent.Repository;

public interface IDiscountRepository
{
    Task<Discount> AddDiscount(Discount discount);
    Task<Discount?> DeleteDiscount(Guid discountId);
    Task<Discount?> GetDiscount(Guid discountId);
    Task<IEnumerable<Discount>> GetDiscount();
    Task<Discount?> UpdateDiscount(Discount discount);
}
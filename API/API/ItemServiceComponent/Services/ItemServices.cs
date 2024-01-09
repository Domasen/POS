using API.DiscountLoyaltyComponent.Models;
using API.DiscountLoyaltyComponent.Services;
using API.ItemServiceComponent.Models;
using API.ItemServiceComponent.Repository;

namespace API.ItemServiceComponent.Services;

public class ItemServices : IItemServices
{
    private readonly IItemRepository _itemRepository;
    private readonly IDiscountServices _discountServices;

    public ItemServices(IItemRepository itemRepository, IDiscountServices discountServices)
    {
        _discountServices = discountServices;
        _itemRepository = itemRepository;
    }
    
    public async Task<Item> AddItem(Item item)
    {
        return await _itemRepository.AddItem(item);
    }

    public async Task<Item?> DeleteItem(Guid itemId)
    {
        return await _itemRepository.DeleteItem(itemId);
    }

    public async Task<Item?> GetItem(Guid itemId)
    {
        return await _itemRepository.GetItem(itemId);
    }

    public async Task<IEnumerable<Item>> GetItems()
    {
        return await _itemRepository.GetItems();
    }

    public async Task<Item?> UpdateItem(Item item)
    {
        return await _itemRepository.UpdateItem(item);
    }

    public async Task<decimal> GetItemPrice(Guid itemId)
    {
        Item? item = await GetItem(itemId);
        
        if (item != null)
        {
            return item.Price;
        }

        return 0;

        // Discount? discount = await _discountServices.GetDiscount((Guid)item.DiscountId);
        //
        // if (discount == null)
        // {
        //     return item.Price;
        // }
        //
        // if (discount.ValidUntil >= DateTime.Today) 
        // {
        //     return item.Price * (1 - ((decimal)discount.DiscountPercentage / 100));
        // }
        //
        // return 0;
    }
}
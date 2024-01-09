using API.ItemServiceComponent.Models;

namespace API.ItemServiceComponent.Services;

public interface IItemServices
{
    Task<Item> AddItem(Item item);
    Task<Item?> DeleteItem(Guid itemId);
    Task<Item?> GetItem(Guid itemId);
    Task<IEnumerable<Item>> GetItems();
    Task<Item?> UpdateItem (Item item);
    Task<decimal> GetItemPrice(Guid itemId);
    Task<decimal> GetItemDiscount(Guid itemId);
}
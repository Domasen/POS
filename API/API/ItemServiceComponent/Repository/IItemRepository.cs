using API.ItemServiceComponent.Models;

namespace API.ItemServiceComponent.Repository;

public interface IItemRepository
{
    Task<Item> AddItem(Item item);
    Task<Item?> DeleteItem(Guid itemId);
    Task<Item?> GetItem(Guid itemId);
    Task<IEnumerable<Item>> GetItems();
    Task<Item?> UpdateItem (Item item);
}
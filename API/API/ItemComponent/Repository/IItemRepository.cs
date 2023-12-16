using API.ItemComponent.Models;

namespace API.ItemComponent.Repository;

public interface IItemRepository
{
    Task<Item> AddItem(Item item);
    Task<Item?> DeleteItem(Guid itemId);
    Task<Item?> GetItem(Guid itemId);
    Task<IEnumerable<Item>> GetItem();
    Task<Item?> UpdateItem (Item item);
}
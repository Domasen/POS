using API.ItemComponent.Models;

namespace API.ItemComponent.Repository;

public interface IItemRepository
{
    Task<Item> AddItem(Item item);
    Task<Item?> DeleteItem(Guid staffId);
    Task<Item?> GetItem(Guid staffId);
    Task<IEnumerable<Item>> GetItem();
    Task<Item?> UpdateItem (Item item);
}
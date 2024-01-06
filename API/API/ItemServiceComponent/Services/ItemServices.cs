using API.ItemServiceComponent.Models;
using API.ItemServiceComponent.Repository;

namespace API.ItemServiceComponent.Services;

public class ItemServices : IItemServices
{
    private readonly IItemRepository _itemRepository;

    public ItemServices(IItemRepository itemRepository)
    {
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
}
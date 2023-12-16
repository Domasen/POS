using API.Data;
using API.ItemComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.ItemComponent.Repository;

public class ItemRepository : IItemRepository
{
    private readonly DataContext _context;
    
    public ItemRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Item> AddItem(Item item)
    {
        item.Id = Guid.NewGuid();
        var result = await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<Item?> DeleteItem(Guid itemId)
    {
        var result = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);

        if (result != null)
        {
            _context.Items.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }
    
    public async Task<Item?> GetItem(Guid itemId)
    {
        return await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
    }

    public async Task<IEnumerable<Item>> GetItem()
    {
        return await _context.Items.ToListAsync();
    }
    
    
    
    public async Task<Item?> UpdateItem(Item item)
    {
        var result = await _context.Items.FirstOrDefaultAsync(i => i.Id == item.Id);

        if (result != null)
        {
            result.Id = item.Id;
            result.DiscountId = item.DiscountId;
            result.Name = item.Name;
            result.Description = item.Description;
            result.Price = item.Price;

            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}
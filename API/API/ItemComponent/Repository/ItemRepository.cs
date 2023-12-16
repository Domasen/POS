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
        item.id = Guid.NewGuid();
        var result = await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<Item?> DeleteItem(Guid itemId)
    {
        var result = await _context.Items.FirstOrDefaultAsync(s => s.id == itemId);

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
        return await _context.Items.FirstOrDefaultAsync(s => s.id == itemId);
    }

    public async Task<IEnumerable<Item>> GetItem()
    {
        return await _context.Items.ToListAsync();
    }
    
    
    
    public async Task<Item?> UpdateItem(Item item)
    {
        var result = await _context.Items.FirstOrDefaultAsync(s => s.id == item.id);

        if (result != null)
        {
            result.id = item.id;
            result.title = item.title;
            result.imageURL = item.imageURL;
            result.description = item.description;
            result.amountInStock = item.amountInStock;
            result.discountPercentage = item.discountPercentage;
            result.price = item.price;
            result.isHidden = item.isHidden;

            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}
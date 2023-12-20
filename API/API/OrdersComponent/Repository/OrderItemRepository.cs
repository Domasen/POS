using API.Data;
using API.OrdersComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.OrdersComponent.Repository;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly DataContext _context;

    public OrderItemRepository(DataContext context)
    {
        _context = context;
    }


    public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
    {
        orderItem.Id = Guid.NewGuid();
        var result = await _context.OrderItems.AddAsync(orderItem);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<OrderItem?> DeleteOrderItem(Guid orderItemId)
    {
        var result = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == orderItemId);

        if (result != null)
        {
            _context.OrderItems.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }

    public async Task<OrderItem?> GetOrderItem(Guid orderItemId)
    {
        return await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == orderItemId);
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItems()
    {
        return await _context.OrderItems.ToListAsync();
    }

    public async Task<OrderItem?> UpdateOrderItem(OrderItem orderItem)
    {
        var result = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == orderItem.Id);

        if (result != null)
        {
            result.Id = orderItem.Id;
            result.OrderId = orderItem.OrderId;
            result.ItemId = orderItem.ItemId;
            result.UnitPrice = orderItem.UnitPrice;
            result.Type = orderItem.Type;
            result.Quantity = orderItem.Quantity;
            result.Subtotal = orderItem.Subtotal;

            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}
using API.Data;
using API.OrdersComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.OrdersComponent.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;
    public OrderRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<Order> AddOrder(Order order)
    {
        order.Id = Guid.NewGuid();
        var result = await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Order?> DeleteOrder(Guid orderId)
    {
        var result = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

        if (result != null)
        {
            _context.Orders.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }

    public async Task<Order?> GetOrder(Guid orderId)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
        var result = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);

        if (result != null)
        {
            result.Id = order.Id;
            result.CustomerId = order.CustomerId;
            result.StaffId = order.StaffId;
            result.Status = order.Status;
            result.Date = order.Date;
            result.TotalAmount = order.TotalAmount;
            result.Tip = order.Tip;

            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}
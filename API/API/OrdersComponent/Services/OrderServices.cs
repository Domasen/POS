using API.Data;
using API.OrdersComponent.Models;
using API.OrdersComponent.Repository;
using API.OrdersComponent.Sevices;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.OrdersComponent.Services;

public class OrderServices : IOrderServices
{
    private readonly IOrderRepository _orderRepository;
    private readonly DataContext _context;
    public OrderServices(IOrderRepository orderRepository, DataContext context)
    {
        _orderRepository = orderRepository;
        _context = context;
    }
    public async Task<Order> AddOrder(Order order)
    {
        return await _orderRepository.AddOrder(order);
    }

    public async Task<Order?> DeleteOrder(Guid orderId)
    {
        return await _orderRepository.DeleteOrder(orderId);
    }
    
    // Gauti susijusius OrderItem'us pagal Order ID
    public async Task<List<OrderItem>> GetOrderItemsByOrderId(Guid orderId)
    {
        return await _context.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();
    }
    
    public async Task<Order?> GetOrder(Guid orderId)
    {
        var order = await _orderRepository.GetOrder(orderId);
        order.TotalAmount = (decimal)await GetOrderTotalAmount(orderId);
        return order;
    }
    
    //suskaičiuoti galutinę sumą 
    public async Task<decimal?> GetOrderTotalAmount(Guid orderId)
    {
        var orderItemsLists =  await GetOrderItemsByOrderId(orderId);
        decimal totalAmount = 0;
        totalAmount = orderItemsLists.Sum(oi => oi.Subtotal);
        return totalAmount;
    }
    //gauna visus orderius su galutinėmis sumomis
    public async Task<IEnumerable<Order>> GetOrdersWithTotalAmount()
    {
        var orders = await _orderRepository.GetOrders();
        foreach (var order in orders)
        {
            order.TotalAmount = (decimal)await GetOrderTotalAmount(order.Id);
        }

        return orders;
    }
    
    public async Task<IEnumerable<Order>> GetOrders()
    {
        var ordersWithTotalAmount = await GetOrdersWithTotalAmount();
        //return await _orderRepository.GetOrders();
        return ordersWithTotalAmount;
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
        return await _orderRepository.UpdateOrder(order);
    }
}
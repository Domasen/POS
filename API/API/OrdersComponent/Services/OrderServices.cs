using API.OrdersComponent.Models;
using API.OrdersComponent.Repository;
using API.OrdersComponent.Sevices;

namespace API.OrdersComponent.Services;

public class OrderServices : IOrderServices
{
    private readonly IOrderRepository _orderRepository;
    
    public OrderServices(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<Order> AddOrder(Order order)
    {
        return await _orderRepository.AddOrder(order);
    }

    public async Task<Order?> DeleteOrder(Guid orderId)
    {
        return await _orderRepository.DeleteOrder(orderId);
    }

    public async Task<Order?> GetOrder(Guid orderId)
    {
        return await _orderRepository.GetOrder(orderId);
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _orderRepository.GetOrders();
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
        return await _orderRepository.UpdateOrder(order);
    }
}
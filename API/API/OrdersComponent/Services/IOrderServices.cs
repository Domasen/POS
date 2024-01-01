using API.OrdersComponent.Models;

namespace API.OrdersComponent.Sevices;

public interface IOrderServices
{
    Task<Order> AddOrder(Order order);
    Task<Order?> DeleteOrder(Guid orderId);
    Task<Order?> GetOrder(Guid orderId);
    Task<IEnumerable<Order>> GetOrders();
    Task<Order?> UpdateOrder (Order order);
}
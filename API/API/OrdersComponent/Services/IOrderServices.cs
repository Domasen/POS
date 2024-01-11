using API.OrdersComponent.Models;

namespace API.OrdersComponent.Services;

public interface IOrderServices
{
    Task<Order> AddOrder(OrderDto order);
    Task<Order?> DeleteOrder(Guid orderId);
    Task<Order?> GetOrder(Guid orderId);
    Task<IEnumerable<Order>> GetOrders();
    Task<Order?> UpdateOrder (Order order);
    Task<Receipt> GetReceipt(Guid orderId);
}
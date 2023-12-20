using API.OrdersComponent.Models;

namespace API.OrdersComponent.Repository;

public interface IOrderItemRepository
{
    Task<OrderItem> AddOrderItem(OrderItem orderItem);
    Task<OrderItem?> DeleteOrderItem(Guid orderItemId);
    Task<OrderItem?> GetOrderItem(Guid orderItemId);
    Task<IEnumerable<OrderItem>> GetOrderItems();
    Task<OrderItem?> UpdateOrderItem (Order orderItem);
}
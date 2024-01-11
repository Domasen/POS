using API.OrdersComponent.Models;

namespace API.OrdersComponent.Services;

public interface IOrderItemServices
{
    Task<OrderItem> AddOrderItem(OrderItemDto orderItem);
    Task<OrderItem?> DeleteOrderItem(Guid orderItemId);
    Task<OrderItem?> GetOrderItem(Guid orderItemId);
    Task<IEnumerable<OrderItem>> GetOrderItems();
    Task<OrderItem?> UpdateOrderItem (OrderItem orderItem);
    Task<List<OrderItem>> GetOrderItemsByOrderId(Guid orderId);
}
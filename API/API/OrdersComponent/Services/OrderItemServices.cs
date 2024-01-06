using API.OrdersComponent.Models;
using API.OrdersComponent.Repository;

namespace API.OrdersComponent.Services;

public class OrderItemServices : IOrderItemServices
{
    private readonly IOrderItemRepository _orderItemRepository;
    public OrderItemServices(IOrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
    }
    public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
    {
        switch (orderItem.Type)
        {
            case OrderItemType.Item:
                break;
            case OrderItemType.Service:
                break;
            case OrderItemType.Appointment:
                break;
        }

        return await _orderItemRepository.AddOrderItem(orderItem);
    }

    public async Task<OrderItem?> DeleteOrderItem(Guid orderItemId)
    {
        return await _orderItemRepository.DeleteOrderItem(orderItemId);
    }

    public async Task<OrderItem?> GetOrderItem(Guid orderItemId)
    {
        return await _orderItemRepository.GetOrderItem(orderItemId);
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItems()
    {
        return await _orderItemRepository.GetOrderItems();
    }

    public async Task<OrderItem?> UpdateOrderItem(OrderItem orderItem)
    {
        return await _orderItemRepository.UpdateOrderItem(orderItem);
    }

    public Decimal CalculateItemPrice(Decimal quantity, Guid itemId)
    {
        return (decimal)0.1;
    }
}
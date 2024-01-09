using API.Data;
using API.ItemServiceComponent.Services;
using API.Migrations;
using API.OrdersComponent.Models;
using API.OrdersComponent.Repository;
using API.TaxComponent.Models;
using API.TaxComponent.Services;
using Microsoft.EntityFrameworkCore;
using Tax = API.TaxComponent.Models.Tax;

namespace API.OrdersComponent.Services;

public class OrderItemServices : IOrderItemServices
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IItemServices _itemServices;
    private readonly ITaxServices _taxServices;
    private readonly DataContext _context;
    public OrderItemServices(IOrderItemRepository orderItemRepository, IItemServices itemServices, ITaxServices taxServices, DataContext context)
    {
        _orderItemRepository = orderItemRepository;
        _itemServices = itemServices;
        _taxServices = taxServices;
        _context = context;
    }
    public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
    {
        switch (orderItem.Type)
        {
            case OrderItemType.Item:
                orderItem.UnitPrice = await _itemServices.GetItemPrice(orderItem.ItemId);
                orderItem.DiscountAmountPerUnit = await _itemServices.GetItemDiscount(orderItem.ItemId);
                orderItem.TaxAmount = await CalculateTax(orderItem.TaxId, orderItem.UnitPrice,
                    orderItem.DiscountAmountPerUnit, orderItem.Quantity);
                orderItem.Subtotal = CalculateSubtotal(orderItem.Quantity, orderItem.UnitPrice, orderItem.DiscountAmountPerUnit, orderItem.TaxAmount);
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

    private Decimal CalculateSubtotal(Decimal quantity, Decimal unitPrice, Decimal discountAmountPerUnit, Decimal taxAmount)
    {
        return ((unitPrice - discountAmountPerUnit) * quantity) + taxAmount;
    }
    
    public async Task<List<OrderItem>> GetOrderItemsByOrderId(Guid orderId)
    {
        return await _context.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();
    }

    private async Task<Decimal> CalculateTax(Guid taxId, Decimal unitPrice, Decimal discountAmountPerUnit, Decimal quantity)
    {
        Tax? tax = await _taxServices.GetTax(taxId);

        if (tax == null || unitPrice == 0)
        {
            return 0;
        }
        
        switch (tax.Category)
        {
            case TaxCategory.Percent:
                return ((unitPrice - discountAmountPerUnit) * quantity) * (((decimal)tax.Value / 100));
            case TaxCategory.Flat:
                return tax.Value;
        }

        return 0;
    }
}
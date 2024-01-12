using API.Data;
using API.ItemServiceComponent.Models;
using API.ItemServiceComponent.Services;
using API.OrdersComponent.Models;
using API.OrdersComponent.Repository;
using API.ServicesComponent.Models;
using API.ServicesComponent.Services;
using Microsoft.EntityFrameworkCore;

namespace API.OrdersComponent.Services;

public class OrderServices : IOrderServices
{
    private readonly IOrderRepository _orderRepository;
    private readonly DataContext _context;
    private readonly IOrderItemServices _orderItemServices;
    private readonly IItemServices _itemServices;
    private readonly IServiceServices _serviceServices;
    private readonly IAppointmentServices _appointmentServices;
    public OrderServices(IOrderRepository orderRepository, DataContext context, IOrderItemServices orderItemServices, IItemServices itemServices, IServiceServices serviceServices, IAppointmentServices appointmentServices)
    {
        _orderRepository = orderRepository;
        _context = context;
        _orderItemServices = orderItemServices;
        _itemServices = itemServices;
        _serviceServices = serviceServices;
        _appointmentServices = appointmentServices;
    }
    public async Task<Order> AddOrder(OrderDto order)
    {
        Order orderCreated = new Order()
        {
            CustomerId = order.CustomerId,
            StaffId = order.StaffId,
            Status = Status.Unpaid,
            Date = order.Date,
            Tip = order.Tip
        };
        return await _orderRepository.AddOrder(orderCreated);
    }
   

    public async Task<Order?> DeleteOrder(Guid orderId)
    {
        return await _orderRepository.DeleteOrder(orderId);
    }
    
    public async Task<Order?> GetOrder(Guid orderId)
    {
        var order = await _orderRepository.GetOrder(orderId);
        order.TotalAmount = (decimal)await GetOrderTotalAmount(orderId);
        return order;
    }
    
    //suskaičiuoti galutinę sumą 
    private async Task<decimal?> GetOrderTotalAmount(Guid orderId)
    {
        var orderItemsLists =  await _orderItemServices.GetOrderItemsByOrderId(orderId);
        Order order = await _orderRepository.GetOrder(orderId);
        decimal totalAmount = 0;
        totalAmount = orderItemsLists.Sum(oi => oi.Subtotal);
        return totalAmount + order.Tip;
    }
    
    //gauna visus orderius su galutinėmis sumomis
    private async Task<IEnumerable<Order>> GetOrdersWithTotalAmount()
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
        return ordersWithTotalAmount;
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
        return await _orderRepository.UpdateOrder(order);
    }
    
    
    public async Task<Receipt> GetReceipt(Guid orderId)
    {
        List<OrderItem> orderItems = await _orderItemServices.GetOrderItemsByOrderId(orderId);
        Order? order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        Receipt receipt = new Receipt();

        if (order != null)
        {
            receipt.OrderId = orderId;
            receipt.ReceiptItems = new List<ReceiptItem>();
            foreach (var orderItem in orderItems)
            {
                receipt.Total += orderItem.Subtotal;
                string name = "";
                switch (orderItem.Type)
                {
                    case OrderItemType.Item:
                        Item? item = await _itemServices.GetItem(orderItem.ItemId);
                        if (item != null)
                        {
                            name = item.Name;
                        }
                        break;
                    case OrderItemType.Service:
                        Service? service = await _serviceServices.GetService(orderItem.ItemId);
                        if (service != null)
                        {
                            name = service.ServiceName;
                        }
                        break;
                    case OrderItemType.Appointment:
                        Appointment? appointment = await _appointmentServices.GetAppointment(orderItem.ItemId);
                        if (appointment != null)
                        {
                            Service? appointmentService = await _serviceServices.GetService(appointment.ServiceId);
                            if (appointmentService != null)
                            {
                                name = appointmentService.ServiceName;
                            } 
                        }

                        break;
                }
                
                ReceiptItem newItem = new ReceiptItem
                {
                    ItemName = name,
                    ItemType = orderItem.Type.ToString(),
                    UnitPrice = orderItem.UnitPrice,
                    DiscountAmountPerUnit = orderItem.DiscountAmountPerUnit,
                    TaxAmount = orderItem.TaxAmount,
                    Quantity = orderItem.Quantity,
                    Subtotal = orderItem.Subtotal
                };
                receipt.ReceiptItems.Add(newItem);
            }

            receipt.Total += order.Tip;
            receipt.Tip = order.Tip;
        }

        return receipt;
    }
}
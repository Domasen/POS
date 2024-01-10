using API.Data;
using API.OrdersComponent.Models;
using API.OrdersComponent.Repository;
using API.OrdersComponent.Sevices;
using API.UsersComponent.Services;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.OrdersComponent.Services;

public class OrderServices : IOrderServices
{
    private readonly IOrderRepository _orderRepository;
    private readonly DataContext _context;
    private readonly IOrderItemServices _orderItemServices;
    private readonly ICustomerServices _customerServices;
    public OrderServices(IOrderRepository orderRepository, DataContext context, IOrderItemServices orderItemServices,  ICustomerServices customerServices)
    {
        _orderRepository = orderRepository;
        _context = context;
        _orderItemServices = orderItemServices;
        _customerServices = customerServices;
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
        var order = await _orderRepository.GetOrder(orderId);
        order.TotalAmount = (decimal)await GetOrderTotalAmount(orderId);
        //order.TotalAmount += order.Tip;
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
        //return await _orderRepository.GetOrders();
        return ordersWithTotalAmount;
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
        return await _orderRepository.UpdateOrder(order);
    }
}
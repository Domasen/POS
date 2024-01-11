using API.OrdersComponent.Models;
using API.OrdersComponent.Services;
using API.PaymentComponent.Models;
using API.PaymentComponent.Repository;
using API.UsersComponent.Services;

namespace API.PaymentComponent.Services;

public class PaymentServices : IPaymentServices
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ICustomerServices _customerServices;
    private readonly IOrderServices _orderServices;
    
    public PaymentServices(IPaymentRepository paymentRepository, ICustomerServices customerServices, IOrderServices orderServices)
    {
        _paymentRepository = paymentRepository;
        _customerServices = customerServices;
        _orderServices = orderServices;
    }
    
    public async Task<Payment> AddPayment(PaymentDto payment)
    {
        Payment paymentCreated = new Payment()
        {
            OrderId = payment.OrderId,
            PaymentMethodId = payment.PaymentMethodId,
            Status = PaymentStatus.Complete,
            Amount = payment.Amount,
            PaymentDate = payment.PaymentDate
        };
        var addPayment =  await _paymentRepository.AddPayment(paymentCreated);
        Order? order = await _orderServices.GetOrder(payment.OrderId);
       
        if (order != null)
        {
            order.Status = Status.Paid;
            paymentCreated.Change = CalculateChange(order.TotalAmount, paymentCreated);
            await UpdatePointsForPurchase(order.CustomerId, order.TotalAmount);
        }

        return addPayment;
        
    }
    
    private decimal CalculateChange(decimal purchaseAmount, Payment payment)
    {
        decimal change = payment.Amount - purchaseAmount;
        return change;
    }
    

    private async Task UpdatePointsForPurchase(Guid customerId, decimal purchaseAmount)
    {
        await _customerServices.UpdatePointsForPurchase(customerId, purchaseAmount);
    }

    public async Task<Payment?> DeletePayment(Guid paymentId)
    {
        return await _paymentRepository.DeletePayment(paymentId);
    }

    public async Task<Payment?> GetPayment(Guid paymentId)
    {
        return await _paymentRepository.GetPayment(paymentId);
    }

    public async Task<IEnumerable<Payment>> GetPayments()
    {
        return await _paymentRepository.GetPayments();
    }

    public async Task<Payment?> UpdatePayment(Payment payment)
    {
        return await _paymentRepository.UpdatePayment(payment);
    }
}
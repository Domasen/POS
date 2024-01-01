using API.PaymentComponent.Models;
using API.PaymentComponent.Repository;

namespace API.PaymentComponent.Services;

public class PaymentServices : IPaymentServices
{
    private readonly IPaymentRepository _paymentRepository;
    
    public PaymentServices(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    
    public async Task<Payment> AddPayment(Payment payment)
    {
        return await _paymentRepository.AddPayment(payment);
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
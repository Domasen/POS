using API.PaymentComponent.Models;

namespace API.PaymentComponent.Services;

public interface IPaymentServices
{
    Task<Payment> AddPayment(Payment payment);
    Task<Payment?> DeletePayment(Guid paymentId);
    Task<Payment?> GetPayment(Guid paymentId);
    Task<IEnumerable<Payment>> GetPayments();
    Task<Payment?> UpdatePayment(Payment payment);
}
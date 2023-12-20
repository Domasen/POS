using API.PaymentComponent.Models;

namespace API.PaymentComponent.Repository;

public interface IPaymentRepository
{
    Task<Payment> AddPayment(Payment payment);
    Task<Payment?> DeletePayment(Guid paymentId);
    Task<Payment?> GetPayment(Guid paymentId);
    Task<IEnumerable<Payment>> GetPayments();
    Task<Payment?> UpdatePayment(Payment payment);
}
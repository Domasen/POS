using API.PaymentComponent.Models;

namespace API.PaymentComponent.Repository;

public interface IPaymentMethodRepository
{
    Task<PaymentMethod> AddPaymentMethod(PaymentMethod paymentMethod);
    Task<PaymentMethod?> DeletePaymentMethod(Guid paymentMethodId);
    Task<PaymentMethod?> GetPaymentMethod(Guid paymentMethodId);
    Task<IEnumerable<PaymentMethod>> GetPaymentMethods();
    Task<PaymentMethod?> UpdatePaymentMethod(PaymentMethod paymentMethod);
}
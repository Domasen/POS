using API.PaymentComponent.Models;
using API.PaymentComponent.Repository;

namespace API.PaymentComponent.Services;

public class PaymentMethodServices : IPaymentMethodServices
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    
    public PaymentMethodServices(IPaymentMethodRepository paymentMethodRepository)
    {
        _paymentMethodRepository = paymentMethodRepository;
    }

    public async Task<PaymentMethod> AddPaymentMethod(PaymentMethod paymentMethod)
    {
        return await _paymentMethodRepository.AddPaymentMethod(paymentMethod);
    }

    public async Task<PaymentMethod?> DeletePaymentMethod(Guid paymentMethodId)
    {
        return await _paymentMethodRepository.DeletePaymentMethod(paymentMethodId);
    }

    public async Task<PaymentMethod?> GetPaymentMethod(Guid paymentMethodId)
    {
        return await _paymentMethodRepository.GetPaymentMethod(paymentMethodId);
    }

    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()
    {
        return await _paymentMethodRepository.GetPaymentMethods();
    }

    public async Task<PaymentMethod?> UpdatePaymentMethod(PaymentMethod paymentMethod)
    {
        return await _paymentMethodRepository.UpdatePaymentMethod(paymentMethod);
    }
}
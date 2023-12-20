using API.Data;
using API.PaymentComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.PaymentComponent.Repository;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly DataContext _context;
    public PaymentMethodRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<PaymentMethod> AddPaymentMethod(PaymentMethod paymentMethod)
    {
        paymentMethod.Id = Guid.NewGuid();
        var result = await _context.PaymentMethods.AddAsync(paymentMethod);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<PaymentMethod?> DeletePaymentMethod(Guid paymentMethodId)
    {
        var result = await _context.PaymentMethods.FirstOrDefaultAsync(p => p.Id == paymentMethodId);

        if (result != null)
        {
            _context.PaymentMethods.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }

    public async Task<PaymentMethod?> GetPaymentMethod(Guid paymentMethodId)
    {
        return await _context.PaymentMethods.FirstOrDefaultAsync(p => p.Id == paymentMethodId);
    }

    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()
    {
        return await _context.PaymentMethods.ToListAsync();
    }

    public async Task<PaymentMethod?> UpdatePaymentMethod(PaymentMethod paymentMethod)
    {
        var result = await _context.PaymentMethods.FirstOrDefaultAsync(p => p.Id == paymentMethod.Id);

        if (result != null)
        {
            result.Id = paymentMethod.Id;
            result.MethodName = paymentMethod.MethodName;
            result.MethodDescription = paymentMethod.MethodDescription;
            await _context.SaveChangesAsync();
        }
        
        return null;
    }
}
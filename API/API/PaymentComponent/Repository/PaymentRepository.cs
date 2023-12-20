using API.Data;
using API.PaymentComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.PaymentComponent.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly DataContext _context;
    public PaymentRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Payment> AddPayment(Payment payment)
    {
        payment.Id = Guid.NewGuid();
        var result = await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Payment?> DeletePayment(Guid paymentId)
    {
        var result = await _context.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);

        if (result != null)
        {
            _context.Payments.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }

    public async Task<Payment?> GetPayment(Guid paymentId)
    {
        return await _context.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
    }

    public async Task<IEnumerable<Payment>> GetPayments()
    {
        return await _context.Payments.ToListAsync();
    }

    public async Task<Payment?> UpdatePayment(Payment payment)
    {
        var result = await _context.Payments.FirstOrDefaultAsync(p => p.Id == payment.Id);

        if (result != null)
        {
            
            result.Id = payment.Id;
            result.OrderId = payment.OrderId;
            result.PaymentMethodId = payment.PaymentMethodId;
            result.Status = payment.Status;
            result.Amount = payment.Amount;
            result.PaymentDate = payment.PaymentDate;
            await _context.SaveChangesAsync();
        }
        
        return null;
    }
}
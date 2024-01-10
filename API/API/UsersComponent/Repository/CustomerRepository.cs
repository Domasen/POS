using API.Data;
using API.UsersComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.UsersComponent.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _context;
    public CustomerRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Customer> AddCustomer(Customer customer)
    {
        customer.Id = Guid.NewGuid();
        var result = await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Customer?> DeleteCustomer(Guid customerId)
    {
        var result = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);

        if (result != null)
        {
            _context.Customers.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }

    public async Task<Customer?> GetCustomer(Guid customerId)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
    }

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> UpdateCustomer(Customer customer)
    {
        var result = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);

        if (result != null)
        {
            result.Id = customer.Id;
            result.LoyaltyId = customer.LoyaltyId;
            result.FirstName = customer.FirstName;
            result.LastName = customer.LastName;
            result.Birthday = customer.Birthday;
            result.Address = customer.Address;
            result.Points = customer.Points;
            await _context.SaveChangesAsync();
        }
        
        return null;
    }

    public async Task<IEnumerable<Customer>> GetCustomersByLoyaltyId(Guid loyaltyId)
    {
        return await _context.Customers.Where(c => c.LoyaltyId == loyaltyId).ToListAsync();
    }
}
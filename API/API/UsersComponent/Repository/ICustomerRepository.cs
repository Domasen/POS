using API.UsersComponent.Models;

namespace API.UsersComponent.Repository;

public interface ICustomerRepository
{
    Task<Customer> AddCustomer(Customer customer);
    Task<Customer?> DeleteCustomer(Guid customerId);
    Task<Customer?> GetCustomer(Guid customerId);
    Task<IEnumerable<Customer>> GetCustomers();
    Task<Customer?> UpdateCustomer(Customer customer);
    Task<IEnumerable<Customer>> GetCustomersByLoyaltyId(Guid loyaltyId);
}
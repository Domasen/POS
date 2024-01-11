using API.UsersComponent.Models;

namespace API.UsersComponent.Services;

public interface ICustomerServices
{
    Task<Customer> AddCustomer(CustomerDto customer);
    Task<Customer?> DeleteCustomer(Guid customerId);
    Task<Customer?> GetCustomer(Guid customerId);
    Task<IEnumerable<Customer>> GetCustomers();
    Task<Customer?> UpdateCustomer(Customer customer);
    Task<IEnumerable<Customer>> GetCustomersByLoyaltyId(Guid loyaltyId);
    Task UpdatePointsForPurchase(Guid customerId, decimal purchaseAmount);
}
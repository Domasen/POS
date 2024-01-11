
using API.DiscountLoyaltyComponent.Repository;
using API.UsersComponent.Models;
using API.UsersComponent.Repository;

namespace API.UsersComponent.Services;

public class CustomerServices : ICustomerServices
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILoyaltyProgramRepository _loyaltyProgramRepository;

    public CustomerServices(ICustomerRepository customerRepository, ILoyaltyProgramRepository loyaltyProgramRepository)
    {
        _customerRepository = customerRepository;
        _loyaltyProgramRepository = loyaltyProgramRepository;
    }

    public async Task<Customer> AddCustomer(CustomerDto customer)
    {
        Customer customerCreated = new Customer()
        {
            LoyaltyId = customer.LoyaltyId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Birthday = customer.Birthday,
            Address = customer.Address
        };
        return await _customerRepository.AddCustomer(customerCreated);
    }

    public async Task<Customer?> DeleteCustomer(Guid customerId)
    {
        return await _customerRepository.DeleteCustomer(customerId);
    }

    public async Task<Customer?> GetCustomer(Guid customerId)
    {
        return await _customerRepository.GetCustomer(customerId);
    }

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await _customerRepository.GetCustomers();
    }

    public async Task<Customer?> UpdateCustomer(Customer customer)
    {
        return await _customerRepository.UpdateCustomer(customer);
    }
    
    public async Task<IEnumerable<Customer>> GetCustomersByLoyaltyId(Guid loyaltyId)
    {
        return await _customerRepository.GetCustomersByLoyaltyId(loyaltyId);
    }
    
    public async Task UpdatePointsForPurchase(Guid customerId, decimal purchaseAmount)
    {
        // Gauti kliento duomenis
        var customer = await _customerRepository.GetCustomer(customerId);

        if (customer != null)
        {
            // Gauti lojalumo programos nustatymus
            var loyaltyProgram = await _loyaltyProgramRepository.GetLoyaltyProgram(customer.LoyaltyId);

            if (loyaltyProgram != null)
            {
                // Atnaujinti taškus pagal pirkimo sumą ir programos nustatymus
                var pointsToAdd = (decimal)(purchaseAmount / loyaltyProgram.PointsPerAmount);
                customer.Points += pointsToAdd;

                // Atnaujinti kliento taškus duomenų bazėje
                await _customerRepository.UpdateCustomer(customer);
            }
        }
    }
}
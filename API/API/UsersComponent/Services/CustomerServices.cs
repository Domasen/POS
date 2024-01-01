using API.UsersComponent.Models;
using API.UsersComponent.Repository;

namespace API.UsersComponent.Services;

public class CustomerServices : ICustomerServices
{
    private readonly ICustomerRepository _customerRepository;
    
    public CustomerServices(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<Customer> AddCustomer(Customer customer)
    {
        return await _customerRepository.AddCustomer(customer);
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
}
using API.DiscountLoyaltyComponent.Models;
using API.DiscountLoyaltyComponent.Services;
using API.ItemServiceComponent.Repository;
using API.ServicesComponent.Models;

namespace API.ServicesComponent.Services;

public class ServiceServices : IServiceServices
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IDiscountServices _discountServices;
    public ServiceServices(IServiceRepository serviceRepository, IDiscountServices discountServices)
    {
        _serviceRepository = serviceRepository;
        _discountServices = discountServices;
    }
    public async Task<Service> AddService(ServiceDto service)
    {
        Service serviceCreated = new Service()
        {
            StaffId = service.StaffId,
            DiscountId = service.DiscountId,
            ServiceName = service.ServiceName,
            ServiceDescription = service.ServiceDescription,
            Duration = service.Duration,
            Price = service.Price
        };
        return await _serviceRepository.AddService(serviceCreated);
    }

    public async Task<Service?> DeleteService(Guid serviceId)
    {
        return await _serviceRepository.DeleteService(serviceId);
    }

    public async Task<Service?> GetService(Guid serviceId)
    {
        return await _serviceRepository.GetService(serviceId);
    }

    public async Task<IEnumerable<Service>> GetServices()
    {
        return await _serviceRepository.GetServices();
    }

    public async Task<Service?> UpdateService(Service service)
    {
        return await _serviceRepository.UpdateService(service);
    }

    public async Task<decimal> GetServicePrice(Guid serviceId)
    {
        Service? service = await GetService(serviceId);

        if (service != null)
        {
            return service.Price;
        }

        return 0;
    }

    public async Task<decimal> GetServiceDiscount(Guid serviceId)
    {
        Service? service = await GetService(serviceId);

        if (service == null)
        {
            return 0;
        }
        
        Discount? discount = await _discountServices.GetDiscount(service.DiscountId);

        if (discount == null)
        {
            return 0;
        }

        if (discount.ValidUntil >= DateTime.Today)
        {
            return service.Price - (service.Price * (1 - (discount.DiscountPercentage / 100)));
        }

        return 0;
    }
}
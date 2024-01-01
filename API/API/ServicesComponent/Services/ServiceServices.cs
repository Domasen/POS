using API.ItemServiceComponent.Repository;
using API.ServicesComponent.Models;

namespace API.ServicesComponent.Services;

public class ServiceServices : IServiceServices
{
    private readonly IServiceRepository _serviceRepository;
    public ServiceServices(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }
    public async Task<Service> AddService(Service service)
    {
        return await _serviceRepository.AddService(service);
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
}
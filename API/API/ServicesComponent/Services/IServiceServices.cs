using API.ServicesComponent.Models;

namespace API.ServicesComponent.Services;

public interface IServiceServices
{
    Task<Service> AddService(Service service);
    Task<Service?> DeleteService(Guid serviceId);
    Task<Service?> GetService(Guid serviceId);
    Task<IEnumerable<Service>> GetServices();
    Task<Service?> UpdateService (Service service);
    Task<decimal> GetServicePrice(Guid serviceId);
    Task<decimal> GetServiceDiscount(Guid serviceId);
}
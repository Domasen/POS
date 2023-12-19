using API.ItemComponent.Models;

namespace API.ItemComponent.Repository;

public interface IServiceRepository
{
    Task<Service> AddService(Service service);
    Task<Service?> DeleteService(Guid serviceId);
    Task<Service?> GetService(Guid serviceId);
    Task<IEnumerable<Service>> GetServices();
    Task<Service?> UpdateService (Service service);
}
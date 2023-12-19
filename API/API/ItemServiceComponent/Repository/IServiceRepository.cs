using API.ItemServiceComponent.Models;

namespace API.ItemServiceComponent.Repository;

public interface IServiceRepository
{
    Task<Service> AddService(Service service);
    Task<Service?> DeleteService(Guid serviceId);
    Task<Service?> GetService(Guid serviceId);
    Task<IEnumerable<Service>> GetServices();
    Task<Service?> UpdateService (Service service);
}
using API.Data;
using API.ItemComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.ItemComponent.Repository;

public class ServiceRepository : IServiceRepository
{
    private readonly DataContext _context;

    public ServiceRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Service> AddService(Service service)
    {
        service.Id = Guid.NewGuid();
        var result = await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    

    public async Task<Service?> DeleteService(Guid serviceId)
    {
        var result = await _context.Services.FirstOrDefaultAsync(s => s.Id == serviceId);

        if (result != null)
        {
            _context.Services.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }

    public async Task<Service?> GetService(Guid serviceId)
    {
        return await _context.Services.FirstOrDefaultAsync(s => s.Id == serviceId);
    }

    public async Task<IEnumerable<Service>> GetService()
    {
        return await _context.Services.ToListAsync();
    }



    public async Task<Service?> UpdateService(Service service)
    {
        var result = await _context.Services.FirstOrDefaultAsync(s => s.Id == service.Id);

        if (result != null)
        {
            result.Id = service.Id;
            result.StaffId = service.StaffId;
            result.DiscountId = service.DiscountId;
            result.ServiceName = service.ServiceName;
            result.ServiceDescription = service.ServiceDescription;
            result.Duration = service.Duration;
            result.Price = service.Price;
            
            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}
using API.ItemServiceComponent.Models;
using API.UsersComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Staff> Staffs => Set<Staff>();

    public DbSet<Item> Items => Set<Item>();
    
    public DbSet<Service> Services => Set<Service>();
}
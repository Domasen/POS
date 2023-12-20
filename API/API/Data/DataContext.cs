using API.DiscountLoyaltyComponent.Models;
using API.ItemServiceComponent.Models;
using API.PaymentComponent.Models;
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
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<LoyaltyProgram> LoyaltyPrograms => Set<LoyaltyProgram>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
}
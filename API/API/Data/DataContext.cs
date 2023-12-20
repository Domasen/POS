﻿using API.DiscountLoyaltyComponent.Models;
using API.ItemServiceComponent.Models;
using API.OrdersComponent.Models;
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
    public DbSet<Order> Orders => Set<Order>();
}
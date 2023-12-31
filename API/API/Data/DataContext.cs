﻿using API.DiscountLoyaltyComponent.Models;
using API.ItemServiceComponent.Models;
using API.OrdersComponent.Models;
using API.PaymentComponent.Models;
using API.ServicesComponent.Models;
using API.TaxComponent.Models;
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
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Tax> Taxes => Set<Tax>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
}
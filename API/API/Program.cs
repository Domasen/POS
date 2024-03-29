using API.Data;
using API.DiscountLoyaltyComponent.Repository;
using API.DiscountLoyaltyComponent.Services;
using API.ItemServiceComponent.Repository;
using API.ItemServiceComponent.Services;
using API.OrdersComponent.Models;
using API.OrdersComponent.Repository;
using API.OrdersComponent.Services;
using API.OrdersComponent.Services;
using API.PaymentComponent.Repository;
using API.PaymentComponent.Services;
using API.ServicesComponent.Services;
using API.TaxComponent.Repository;
using API.TaxComponent.Services;
using API.UsersComponent.Models;
using API.UsersComponent.Repository;
using API.UsersComponent.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    // Configure Swagger to use enum names instead of numbers
    c.UseInlineDefinitionsForEnums();
});
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<ILoyaltyProgramRepository, LoyaltyProgramRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IStaffServices, StaffServices>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();
builder.Services.AddScoped<IPaymentMethodServices, PaymentMethodServices>();
builder.Services.AddScoped<IDiscountServices, DiscountServices>();
builder.Services.AddScoped<ILoyaltyProgramServices, LoyaltyProgramServices>();
builder.Services.AddScoped<IServiceServices, ServiceServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();
builder.Services.AddScoped<ITaxRepository, TaxRepository>();
builder.Services.AddScoped<ITaxServices, TaxServices>();
builder.Services.AddScoped<IAppointmentServices, AppointmentServices>();
builder.Services.AddScoped<IItemServices, ItemServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
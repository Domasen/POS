namespace API.ServicesComponent.Models;

public class ServiceDto
{
    public Guid StaffId { get; set; }
    public Guid DiscountId { get; set; }
    public string ServiceName { get; set; } = String.Empty;
    public string ServiceDescription { get; set; } = String.Empty;
    public int Duration { get; set; }
    public decimal Price { get; set; } 
}
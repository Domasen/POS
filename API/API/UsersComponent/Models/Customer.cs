namespace API.UsersComponent.Models;

public class Customer
{
    public Guid Id { get; set; }
    public Guid DiscountId { get; set; }
    public string Name { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public DateTime Birthday { get; set; }
    public string Address { get; set; } = String.Empty;
    public int Points { get; set; }

}
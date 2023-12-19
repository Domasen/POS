namespace API.UsersComponent.Models;

public class Customer
{
    public Guid Id { get; set; }
    public Guid LoyaltyId { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public DateTime Birthday { get; set; }
    public string Address { get; set; } = String.Empty;
    public int Points { get; set; }

}
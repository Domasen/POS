namespace API.UsersComponent.Models;

public class CustomerDto
{
    public Guid LoyaltyId { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public DateTime Birthday { get; set; }
    public string Address { get; set; } = String.Empty;
}
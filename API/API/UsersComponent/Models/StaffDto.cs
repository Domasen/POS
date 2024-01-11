namespace API.UsersComponent.Models;

public class StaffDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public DateTime HireDate { get; set; } = DateTime.Today;
}
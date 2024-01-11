namespace API.ServicesComponent.Models;

public class AppointmentDto
{
    public Guid CustomerId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime ReservationTime { get; set; }
}
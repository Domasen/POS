namespace API.ServicesComponent.Models;

public class Appointment
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ServiceId { get; set; }
    public Guid EmployeeId { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateTime ReservationTime { get; set; } 
    public DateTime EndTime { get; set; }
    public decimal Duration { get; set; }
   
}
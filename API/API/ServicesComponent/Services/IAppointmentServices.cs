using API.ServicesComponent.Models;

namespace API.ServicesComponent.Services;

public interface IAppointmentServices
{
    Task<Appointment> AddAppointment(Appointment appointment);
    Task<Appointment?> DeleteAppointment(Guid appointmentId);
    Task<Appointment?> GetAppointment(Guid appointmentId);
    Task<IEnumerable<Appointment>> GetAppointments();
    Task<Appointment?> UpdateAppointment (Appointment appointment);
}
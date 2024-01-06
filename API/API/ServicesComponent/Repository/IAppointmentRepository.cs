using API.ServicesComponent.Models;

namespace API.ItemServiceComponent.Repository;

public interface IAppointmentRepository
{
    Task<Appointment> AddAppointment(Appointment appointment);
    Task<Appointment?> DeleteAppointment(Guid loyaltyProgramId);
    Task<Appointment?> GetAppointment(Guid loyaltyProgramId);
    Task<IEnumerable<Appointment>> GetAppointments();
    Task<Appointment?> UpdateAppointment(Appointment appointment);
    
}
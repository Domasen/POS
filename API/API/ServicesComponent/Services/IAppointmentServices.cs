using API.ServicesComponent.Models;

namespace API.ServicesComponent.Services;

public interface IAppointmentServices
{
    Task<Appointment> AddAppointment(Appointment appointment);
    Task<Appointment?> DeleteAppointment(Guid appointmentId);
    Task<Appointment?> GetAppointment(Guid appointmentId);
    Task<IEnumerable<Appointment>> GetAppointments();
    Task<Appointment?> UpdateAppointment (Appointment appointment);
    Task<IEnumerable<TimeSlot>> GetFreeTimes(Guid serviceId, Guid staffId, DateOnly appointmentDate,
        TimeOnly startingTime, TimeOnly endTime);

    // Task<Boolean> CheckSlotAvailability(Guid serviceId, Guid staffId, DateTime reservationTime);
}
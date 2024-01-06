using API.ItemServiceComponent.Repository;
using API.ServicesComponent.Models;

namespace API.ServicesComponent.Services;

public class AppointmentServices : IAppointmentServices
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentServices(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
    public async Task<Appointment> AddAppointment(Appointment appointment)
    {
        return await _appointmentRepository.AddAppointment(appointment);
    }

    public async Task<Appointment?> DeleteAppointment(Guid appointmentId)
    {
        return await _appointmentRepository.DeleteAppointment(appointmentId);
    }

    public async Task<Appointment?> GetAppointment(Guid appointmentId)
    {
        return await _appointmentRepository.GetAppointment(appointmentId);
    }

    public async Task<IEnumerable<Appointment>> GetAppointments()
    {
        return await _appointmentRepository.GetAppointments();
    }

    public async Task<Appointment?> UpdateAppointment(Appointment appointment)
    {
        return await _appointmentRepository.UpdateAppointment(appointment);
    }
}
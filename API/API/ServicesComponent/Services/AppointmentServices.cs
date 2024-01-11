using API.Data;
using API.ItemServiceComponent.Repository;
using API.ServicesComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.ServicesComponent.Services;

public class AppointmentServices : IAppointmentServices
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IServiceServices _serviceServices;
    private readonly DataContext _context;

    public AppointmentServices(IAppointmentRepository appointmentRepository, IServiceServices serviceServices, DataContext context)
    {
        _appointmentRepository = appointmentRepository;
        _serviceServices = serviceServices;
        _context = context;
    }
    public async Task<Appointment> AddAppointment(Appointment appointment)
    {
        int duration = await GetAppointmentDuration(appointment.ServiceId);
        DateTime startTime = appointment.ReservationTime;
        appointment.Duration = duration;
        appointment.EndTime = startTime.AddMinutes(duration);
        appointment.Status = AppointmentStatus.Open;
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
    
    public async Task<IEnumerable<TimeSlot>> GetFreeTimes(Guid serviceId, Guid staffId, DateOnly appointmentDate, TimeOnly startingTime, TimeOnly endTime)
    {
        var appointments = await _context.Appointments
            .Where(a => a.EmployeeId == staffId &&
                        a.ServiceId == serviceId &&
                        a.Status == AppointmentStatus.Open &&
                        a.ReservationTime.Date == appointmentDate.ToDateTime(TimeOnly.MinValue))
            .OrderBy(a => a.ReservationTime)
            .ToListAsync();

        var freeTimeSlots = new List<TimeSlot>();
        var currentTime = startingTime;

        foreach (var appointment in appointments)
        {
            var appointmentStart = appointment.ReservationTime.TimeOfDay;
            var appointmentEnd = appointment.EndTime.TimeOfDay;

            // Fill in any gap before the appointment
            while (currentTime.ToTimeSpan() < appointmentStart && currentTime.ToTimeSpan() < endTime.ToTimeSpan())
            {
                var nextTime = currentTime.AddMinutes(30);
                if (nextTime.ToTimeSpan() > appointmentStart)
                {
                    nextTime = new TimeOnly(appointmentStart.Hours, appointmentStart.Minutes);
                }

                if (nextTime <= endTime)
                {
                    freeTimeSlots.Add(new TimeSlot { StartTime = currentTime, EndTime = nextTime });
                }
            
                currentTime = nextTime;
            }

            // Skip the appointment
            currentTime = new TimeOnly(appointmentEnd.Hours, appointmentEnd.Minutes);
        }

        // Fill in any remaining time after the last appointment
        while (currentTime < endTime)
        {
            var nextTime = currentTime.AddMinutes(30);
            if (nextTime > endTime) // Adjust the last slot to not exceed the end time
            {
                nextTime = endTime;
            }

            freeTimeSlots.Add(new TimeSlot { StartTime = currentTime, EndTime = nextTime });
            currentTime = nextTime;
        }

        return freeTimeSlots;
    }

    // public Task<bool> CheckSlotAvailability(Guid serviceId, Guid staffId, DateTime reservationTime)
    // {
    //     throw new NotImplementedException();
    // }

    private async Task<int> GetAppointmentDuration(Guid serviceId)
    {
        Service? service = await _serviceServices.GetService(serviceId);

        if (service != null)
        {
            return service.Duration;
        }

        return 0;
    }
}
using API.Data;
using API.ServicesComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace API.ItemServiceComponent.Repository;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly DataContext _context;

    public AppointmentRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<Appointment> AddAppointment(Appointment appointment)
    {
        appointment.Id = Guid.NewGuid();
        var result = await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Appointment?> DeleteAppointment(Guid appointmentId)
    {
        var result = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId);
        if (result != null)
        {
            _context.Appointments.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        return result;
    }

    public async Task<Appointment?> GetAppointment(Guid appointmentId)
    {
        return await _context.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId);
    }

    public async Task<IEnumerable<Appointment>> GetAppointments()
    {
        return await _context.Appointments.ToListAsync();
    }

    public async Task<Appointment?> UpdateAppointment(Appointment appointment)
    {
        var result = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == appointment.Id);

        if (result != null)
        {
            result.Id = appointment.Id;
            result.CustomerId = appointment.CustomerId;
            result.ServiceId = appointment.ServiceId;
            result.EmployeeId = appointment.EmployeeId;
            result.ReservationTime = appointment.ReservationTime;
            result.EndTime = appointment.EndTime;
            result.Duration = appointment.Duration;

            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}
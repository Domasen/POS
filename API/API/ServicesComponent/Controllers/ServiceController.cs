using API.ServicesComponent.Models;
using API.ServicesComponent.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.ServicesComponent.Controllers;
// https://www.pragimtech.com/blog/blazor/delete-in-asp.net-core-rest-api/
[ApiController]
[Route("[controller]")]

public class ServiceController : ControllerBase
{
    private readonly ILogger<ServiceController> _logger;
    private readonly IServiceServices _serviceServices;
    private readonly IAppointmentServices _appointmentServices;
    
    public ServiceController(ILogger<ServiceController> logger, IServiceServices serviceServices, IAppointmentServices appointmentServices)
    {
        _logger = logger;
        _serviceServices = serviceServices;
        _appointmentServices = appointmentServices;
    }
    [HttpPost("Service")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    
    public async Task<ActionResult<Service>> CreateService(Service? service)
    {
        try
        {
            if (service == null)
            {
                return BadRequest();
            }

            var createdService = await _serviceServices.AddService(service);

            return CreatedAtAction(nameof(GetServices), new { id = createdService.Id }, createdService);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Service/Services")]
    public async Task<ActionResult<List<Service>>> GetServices()
    {
        try
        {
            return Ok(await _serviceServices.GetServices());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Service/{id}")]
    public async Task<ActionResult<Service>> GetService(Guid id)
    {
        try
        {
            var result = await _serviceServices.GetService(id);
    
            if (result == null)
            {
                return NotFound($"Service with Id = {id} not found");
            }
    
            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpDelete("Service/{Id}")]
    public async Task<ActionResult<Service>> DeleteService(Guid id)
    {
        try
        {
            var serviceToDelete = await _serviceServices.GetService(id);

            if (serviceToDelete == null)
            {
                return NotFound($"Service with Id = {id} not found");
            }

            return await _serviceServices.DeleteService(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("Service/{id}")]
    public async Task<ActionResult<Service>> UpdateService(Guid id, Service service)
    {
        try
        {
            if(id != service.Id)
            {
                return BadRequest("Service ID mismatch");
            }

            var serviceToUpdate = await _serviceServices.GetService(id);

            if(serviceToUpdate == null)
            {
                return NotFound($"Service with Id = {id} not found");
            }

            return await _serviceServices.UpdateService(service);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
    
    [HttpPost("Appointment")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Appointment>> CreateAppointment(Appointment? appointment)
    {
        try
        {
            if (appointment == null)
            {
                return BadRequest();
            }

            Boolean isSlotAvailable = await _appointmentServices.CheckSlotAvailability(appointment.ServiceId,
                appointment.EmployeeId,
                appointment.ReservationTime);
            
            if (isSlotAvailable == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "There is no available time slot for this appointment. Please check available time slots");
            }

            var createdAppointment = await _appointmentServices.AddAppointment(appointment);

            return CreatedAtAction(nameof(GetAppointment), new { id = createdAppointment.Id }, createdAppointment);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Appointment/Appointments")]
    public async Task<ActionResult<List<Appointment>>> GetAppointments()
    {
        try
        {
            return Ok(await _appointmentServices.GetAppointments());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Appointment/{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
    {
        try
        {
            var result = await _appointmentServices.GetAppointment(id);

            if (result == null)
            {
                return NotFound($"Appointment with Id = {id} not found");
            }

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpDelete("Appointment/{id}")]
    public async Task<ActionResult<Appointment>> DeleteAppointment(Guid id)
    {
        try
        {
            var appointmentToDelete = await _appointmentServices.GetAppointment(id);

            if (appointmentToDelete == null)
            {
                return NotFound($"Appointment with Id = {id} not found");
            }

            return await _appointmentServices.DeleteAppointment(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("Appointment/{id}")]
    public async Task<ActionResult<Appointment>> UpdateAppointment(Guid id, Appointment appointment)
    {
        try
        {
            if(id != appointment.Id)
            {
                return BadRequest("Appointment ID mismatch");
            }

            var appointmentToUpdate = await _appointmentServices.GetAppointment(id);

            if(appointmentToUpdate == null)
            {
                return NotFound($"Appointment with Id = {id} not found");
            }

            return await _appointmentServices.UpdateAppointment(appointment);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
    
    [HttpGet("Appointment/FreeSlots")]
    public async Task<ActionResult<List<TimeSlot>>> GetFreeSlots(Guid serviceId, Guid staffId, DateTime appointmentDate)
    {
        try
        {
            return Ok(await _appointmentServices.GetFreeTimes(serviceId, staffId, DateOnly.FromDateTime(appointmentDate), new TimeOnly(9,0), new TimeOnly(17, 0)));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
}
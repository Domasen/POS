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
    
    public ServiceController(ILogger<ServiceController> logger, IServiceServices serviceServices)
    {
        _logger = logger;
        _serviceServices = serviceServices;
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
 
}
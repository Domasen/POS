using API.Data;
using API.ItemComponent.Models;
using API.ItemComponent.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.ItemComponent.Controllers;
// https://www.pragimtech.com/blog/blazor/delete-in-asp.net-core-rest-api/
[ApiController]
[Route("[controller]")]

public class ServiceController : ControllerBase
{
    private readonly ILogger<ServiceController> _logger;
    private readonly IServiceRepository _serviceRepository;
    
    public ServiceController(ILogger<ServiceController> logger, IServiceRepository serviceRepository)
    {
        _logger = logger;
        _serviceRepository = serviceRepository;
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

            var createdService = await _serviceRepository.AddService(service);

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
            return Ok(await _serviceRepository.GetServices());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Service/{Id}")]
    public async Task<ActionResult<Service>> GetService(Guid id)
    {
        try
        {
            var result = await _serviceRepository.GetService(id);

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
            var serviceToDelete = await _serviceRepository.GetService(id);

            if (serviceToDelete == null)
            {
                return NotFound($"Service with Id = {id} not found");
            }

            return await _serviceRepository.DeleteService(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("Service/{Id}")]
    public async Task<ActionResult<Service>> UpdateService(Guid Id, Service service)
    {
        try
        {
            if(Id != service.Id)
            {
                return BadRequest("Service ID mismatch");
            }

            var serviceToUpdate = await _serviceRepository.GetService(Id);

            if(serviceToUpdate == null)
            {
                return NotFound($"Service with Id = {Id} not found");
            }

            return await _serviceRepository.UpdateService(service);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
 
}
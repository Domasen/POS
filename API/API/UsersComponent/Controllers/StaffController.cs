using API.Data;
using API.UsersComponent.Models;
using API.UsersComponent.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.UsersComponent.Controllers;
// https://www.pragimtech.com/blog/blazor/delete-in-asp.net-core-rest-api/
[ApiController]
[Route("[controller]")]
public class StaffController : ControllerBase
{
    private readonly ILogger<StaffController> _logger;
    private readonly IStaffRepository _staffRepository;
    public StaffController(ILogger<StaffController> logger, IStaffRepository staffRepository)
    {
        _logger = logger;
        _staffRepository = staffRepository;
    }
    
    [HttpPost("Staff")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Staff>> CreateStaff(Staff? staff)
    {
        try
        {
            if (staff == null)
            {
                return BadRequest();
            }

            var createdStaff = await _staffRepository.AddStaff(staff);

            return CreatedAtAction(nameof(GetStaff), new { id = createdStaff.Id }, createdStaff);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving dara from the database");
        }
    } 
    
    [HttpGet("Staff/Staffs")]
    public async Task<ActionResult<List<Staff>>> GetStaffs()
    {
        try
        {
            return Ok(await _staffRepository.GetStaffs());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Staff/{id}")]
    public async Task<ActionResult<Staff>> GetStaff(Guid id)
    {
        try
        {
            var result = await _staffRepository.GetStaff(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpDelete("Staff/{id}")]
    public async Task<ActionResult<Staff>> DeleteStaff(Guid id)
    {
        try
        {
            var staffToDelete = await _staffRepository.GetStaff(id);

            if (staffToDelete == null)
            {
                return NotFound($"Staff with Id = {id} not found");
            }

            return await _staffRepository.DeleteStaff(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("Staff/{id}")]
    public async Task<ActionResult<Staff>> UpdateStaff(Guid id, Staff staff)
    {
        try
        {
            if(id != staff.Id)
            {
                return BadRequest("Employee ID mismatch");
            }

            var staffToUpdate = await _staffRepository.GetStaff(id);

            if(staffToUpdate == null)
            {
                return NotFound($"Staff with Id = {id} not found");
            }

            return await _staffRepository.UpdateStaff(staff);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
    
    
}
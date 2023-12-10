using API.UsersComponent.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.UsersComponent.Controllers;

[ApiController]
[Route("[controller]")]
public class StaffController : ControllerBase
{
    private readonly ILogger<StaffController> _logger;

    public StaffController(ILogger<StaffController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost("Staff")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<Staff> CreateStaff([FromBody] Staff createStaffDto)
    {

        var staff = new Staff
        {
            Id = Guid.NewGuid(),
            FirstName = createStaffDto.FirstName,
            LastName = createStaffDto.LastName,
            PhoneNumber = createStaffDto.PhoneNumber,
            Email = createStaffDto.Email,
            HireDate = createStaffDto.HireDate
            
        };

        return Ok(staff);
    } 
    
    
}
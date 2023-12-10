using API.Data;
using API.UsersComponent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.UsersComponent.Controllers;

[ApiController]
[Route("[controller]")]
public class StaffController : ControllerBase
{
    private readonly ILogger<StaffController> _logger;
    private readonly DataContext _context;
    public StaffController(ILogger<StaffController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpPost("Staff")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Staff>> CreateStaff(Staff staff)
    {
        staff.Id = Guid.NewGuid();
        _context.Staffs.Add(staff);
        await _context.SaveChangesAsync();

        return Ok(await _context.Staffs.FindAsync(staff.Id));
    } 
    
    [HttpGet("Staff/Staffs")]
    public async Task<ActionResult<List<Staff>>> GetStaffs()
    {
        return Ok(await _context.Staffs.ToListAsync());
    }
    
    [HttpGet("Staff/{id}")]
    public async Task<ActionResult<Staff>> GetStaff(Guid id)
    {
        var staff = await _context.Staffs.FindAsync(id);
        if (staff == null)
        {
            return BadRequest("Staff not found.");
        }
        return Ok(staff);
    } 
}
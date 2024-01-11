using API.UsersComponent.Models;
using API.UsersComponent.Repository;
using API.UsersComponent.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.UsersComponent.Controllers;
// https://www.pragimtech.com/blog/blazor/delete-in-asp.net-core-rest-api/
[ApiController]
[Route("[controller]")]
public class CustomerStaffController : ControllerBase
{
    private readonly ILogger<CustomerStaffController> _logger;
    private readonly IStaffServices _staffServices;
    private readonly ICustomerServices _customerServices;
    public CustomerStaffController(ILogger<CustomerStaffController> logger, IStaffServices staffServices, ICustomerServices customerServices)
    {
        _logger = logger;
        _staffServices = staffServices;
        _customerServices = customerServices;
    }
    
    [HttpPost("Staff")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Staff>> CreateStaff(StaffDto? staff)
    {
        try
        {
            if (staff == null)
            {
                return BadRequest();
            }

            var createdStaff = await _staffServices.AddStaff(staff);

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
            return Ok(await _staffServices.GetStaffs());
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
            var result = await _staffServices.GetStaff(id);

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
            var staffToDelete = await _staffServices.GetStaff(id);

            if (staffToDelete == null)
            {
                return NotFound($"Staff with Id = {id} not found");
            }

            return await _staffServices.DeleteStaff(id);
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
                return BadRequest("Staff ID mismatch");
            }

            var staffToUpdate = await _staffServices.GetStaff(id);

            if(staffToUpdate == null)
            {
                return NotFound($"Staff with Id = {id} not found");
            }

            return await _staffServices.UpdateStaff(staff);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
     /////////////////Customer
     
     [HttpPost("Customer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerDto? customer)
    {
        try
        {
            if (customer == null)
            {
                return BadRequest();
            }

            var createdCustomer = await _customerServices.AddCustomer(customer);

            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, createdCustomer);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving dara from the database");
        }
    } 
    
    [HttpGet("Customer/Customers")]
    public async Task<ActionResult<List<Customer>>> GetCustomers()
    {
        try
        {
            return Ok(await _customerServices.GetCustomers());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Customer/{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(Guid id)
    {
        try
        {
            var result = await _customerServices.GetCustomer(id);

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

    [HttpDelete("Customer/{id}")]
    public async Task<ActionResult<Customer>> DeleteCustomer(Guid id)
    {
        try
        {
            var customerToDelete = await _customerServices.GetCustomer(id);

            if (customerToDelete == null)
            {
                return NotFound($"Customer with Id = {id} not found");
            }

            return await _customerServices.DeleteCustomer(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("Customer/{id}")]
    public async Task<ActionResult<Customer>> UpdateCustomer(Guid id, Customer customer)
    {
        try
        {
            if(id != customer.Id)
            {
                return BadRequest("Staff ID mismatch");
            }

            var customerToUpdate = await _customerServices.GetCustomer(id);

            if(customerToUpdate == null)
            {
                return NotFound($"Customer with Id = {id} not found");
            }

            return await _customerServices.UpdateCustomer(customer);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }

}
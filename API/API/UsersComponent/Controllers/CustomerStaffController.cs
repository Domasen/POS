using API.Data;
using API.UsersComponent.Models;
using API.UsersComponent.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.UsersComponent.Controllers;
// https://www.pragimtech.com/blog/blazor/delete-in-asp.net-core-rest-api/
[ApiController]
[Route("[controller]")]
public class CustomerStaffController : ControllerBase
{
    private readonly ILogger<CustomerStaffController> _logger;
    private readonly IStaffRepository _staffRepository;
    private readonly ICustomerRepository _customerRepository;
    public CustomerStaffController(ILogger<CustomerStaffController> logger, IStaffRepository staffRepository, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _staffRepository = staffRepository;
        _customerRepository = customerRepository;
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
                return BadRequest("Staff ID mismatch");
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
     /////////////////Customer
     
     [HttpPost("Customer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer? customer)
    {
        try
        {
            if (customer == null)
            {
                return BadRequest();
            }

            var createdCustomer = await _customerRepository.AddCustomer(customer);

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
            return Ok(await _customerRepository.GetCustomers());
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
            var result = await _customerRepository.GetCustomer(id);

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
            var customerToDelete = await _customerRepository.GetCustomer(id);

            if (customerToDelete == null)
            {
                return NotFound($"Customer with Id = {id} not found");
            }

            return await _customerRepository.DeleteCustomer(id);
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

            var customerToUpdate = await _customerRepository.GetCustomer(id);

            if(customerToUpdate == null)
            {
                return NotFound($"Customer with Id = {id} not found");
            }

            return await _customerRepository.UpdateCustomer(customer);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }

}
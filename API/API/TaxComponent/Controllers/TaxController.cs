using API.TaxComponent.Models;
using API.TaxComponent.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.TaxComponent.Controllers;

public class TaxController : ControllerBase
{
    private readonly ILogger<TaxController> _logger;
    private readonly ITaxServices _taxServices;

    public TaxController(ILogger<TaxController> logger, ITaxServices taxServices)
    {
        _logger = logger;
        _taxServices = taxServices;
    }
    
    [HttpPost("Tax")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    
    public async Task<ActionResult<Tax>> CreateTax([FromBody]Tax? tax)
    {
        try
        {
            if (tax == null)
            {
                return BadRequest();
            }

            var createdTax = await _taxServices.AddTax(tax);

            return CreatedAtAction(nameof(GetTax), new { id = createdTax.Id }, createdTax);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Tax/Taxes")]
    public async Task<ActionResult<List<Tax>>> GetTaxes()
    {
        try
        {
            return Ok(await _taxServices.GetTaxes());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Tax/{id}")]
    public async Task<ActionResult<Tax>> GetTax(Guid id)
    {
        try
        {
            var result = await _taxServices.GetTax(id);
    
            if (result == null)
            {
                return NotFound($"Tax with Id = {id} not found");
            }
    
            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpDelete("Tax/{id}")]
    public async Task<ActionResult<Tax>> DeleteTax(Guid id)
    {
        try
        {
            var taxToDelete = await _taxServices.GetTax(id);

            if (taxToDelete == null)
            {
                return NotFound($"Tax with Id = {id} not found");
            }

            return await _taxServices.DeleteTax(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("Tax/{id}")]
    public async Task<ActionResult<Tax>> UpdateTax(Guid id,[FromBody]Tax tax)
    {
        try
        {
            if(id != tax.Id)
            {
                return BadRequest("Tax ID mismatch");
            }

            var taxToUpdate = await _taxServices.GetTax(id);

            if(taxToUpdate == null)
            {
                return NotFound($"Tax with Id = {id} not found");
            }

            return await _taxServices.UpdateTax(tax);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
}
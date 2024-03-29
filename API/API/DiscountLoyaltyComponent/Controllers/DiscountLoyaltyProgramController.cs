﻿using API.DiscountLoyaltyComponent.Models;
using API.DiscountLoyaltyComponent.Repository;
using API.DiscountLoyaltyComponent.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.DiscountLoyaltyComponent.Controllers;
// https://www.pragimtech.com/blog/blazor/delete-in-asp.net-core-rest-api/
[ApiController]
[Route("[controller]")]

public class DiscountLoyaltyProgramController : ControllerBase
{
    private readonly ILogger<DiscountLoyaltyProgramController> _logger;
    private readonly IDiscountServices _discountServices;
    private readonly ILoyaltyProgramServices _loyaltyProgramServices;
    
    public DiscountLoyaltyProgramController(ILogger<DiscountLoyaltyProgramController> logger, IDiscountServices discountServices, ILoyaltyProgramServices loyaltyProgramServices)
    {
        _logger = logger;
        _discountServices = discountServices;
        _loyaltyProgramServices = loyaltyProgramServices;
    }
    
    [HttpPost("LoyaltyProgram")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<LoyaltyProgram>> CreateLoyaltyProgram(LoyaltyProgramDto? loyaltyProgram)
    {
        try
        {
            if (loyaltyProgram == null)
            {
                return BadRequest();
            }

            var createdLoyaltyProgram = await _loyaltyProgramServices.AddLoyaltyProgram(loyaltyProgram);

            return CreatedAtAction(nameof(GetLoyaltyProgram), new { id = createdLoyaltyProgram.Id }, createdLoyaltyProgram);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        }
    }
    
    [HttpGet("LoyaltyProgram/LoyaltyPrograms")]
    public async Task<ActionResult<List<LoyaltyProgram>>> GetLoyaltyPrograms()
    {
        try
        {
            return Ok(await _loyaltyProgramServices.GetLoyaltyPrograms());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    
    
    [HttpGet("LoyaltyProgram/{id}")]
    public async Task<ActionResult<LoyaltyProgram>> GetLoyaltyProgram(Guid id)
    {
        try
        {
            var result = await _loyaltyProgramServices.GetLoyaltyProgram(id);

            if (result == null)
            {
                return NotFound($"LoyaltyProgram with Id = {id} not found");
            }

            
            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpDelete("LoyaltyProgram/{id}")]
    public async Task<ActionResult<LoyaltyProgram>> DeleteLoyaltyProgram(Guid id)
    {
        try
        {
            var loyaltyProgramToDelete = await _loyaltyProgramServices.GetLoyaltyProgram(id);

            if (loyaltyProgramToDelete == null)
            {
                return NotFound($"LoyaltyProgram with Id = {id} not found");
            }

            return await _loyaltyProgramServices.DeleteLoyaltyProgram(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }

    [HttpPut("LoyaltyProgram/{id}")]
    public async Task<ActionResult<LoyaltyProgram>> UpdateLoyaltyProgram(Guid id, LoyaltyProgram loyaltyProgram)
    {
        try
        {
            if(id != loyaltyProgram.Id)
            {
                return BadRequest("LoyaltyProgram ID mismatch");
            }

            var loyaltyProgramToUpdate = await _loyaltyProgramServices.GetLoyaltyProgram(id);

            if(loyaltyProgramToUpdate == null)
            {
                return NotFound($"LoyaltyProgram with Id = {id} not found");
            }

            return await _loyaltyProgramServices.UpdateLoyaltyProgram(loyaltyProgram);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
    
    [HttpPost("Discount")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Discount>> CreateDiscount(DiscountDto? discount)
    {
        try
        {
            if (discount == null)
            {
                return BadRequest();
            }

            var createdDiscount = await _discountServices.AddDiscount(discount);

            return CreatedAtAction(nameof(GetDiscount), new { id = createdDiscount.Id }, createdDiscount);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        }
    }
 
    [HttpGet("Discount/Discounts")]
    public async Task<ActionResult<List<Discount>>> GetDiscounts()
    {
        try
        {
            return Ok(await _discountServices.GetDiscounts());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Discount/{id}")]
    public async Task<ActionResult<Discount>> GetDiscount(Guid id)
    {
        try
        {
            var result = await _discountServices.GetDiscount(id);

            if (result == null)
            {
                return NotFound($"Discount with Id = {id} not found");
            }

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpDelete("Discount/{id}")]
    public async Task<ActionResult<Discount>> DeleteDiscount(Guid id)
    {
        try
        {
            var discountToDelete = await _discountServices.GetDiscount(id);

            if (discountToDelete == null)
            {
                return NotFound($"Discount with Id = {id} not found");
            }

            return await _discountServices.DeleteDiscount(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("Discount/{id}")]
    public async Task<ActionResult<Discount>> UpdateDiscount(Guid id, Discount discount)
    {
        try
        {
            if(id != discount.Id)
            {
                return BadRequest("Discount ID mismatch");
            }

            var discountToUpdate = await _discountServices.GetDiscount(id);

            if(discountToUpdate == null)
            {
                return NotFound($"Discount with Id = {id} not found");
            }

            return await _discountServices.UpdateDiscount(discount);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
}
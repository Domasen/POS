﻿using API.ItemServiceComponent.Models;
using API.ItemServiceComponent.Repository;
using API.ItemServiceComponent.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.ItemServiceComponent.Controllers;
// https://www.pragimtech.com/blog/blazor/delete-in-asp.net-core-rest-api/
[ApiController]
[Route("[controller]")]

public class ItemController : ControllerBase
{
    private readonly ILogger<ItemController> _logger;
    private readonly IItemServices _itemServices;
    
    public ItemController(ILogger<ItemController> logger, IItemServices itemServices)
    {
        _logger = logger;
        _itemServices = itemServices;
    }
    [HttpPost("Item")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Item>> CreateItem(ItemDto? item)
    {
        try
        {
            if (item == null)
            {
                return BadRequest();
            }

            var createdItem = await _itemServices.AddItem(item);

            return CreatedAtAction(nameof(GetItems), new { id = createdItem.Id }, createdItem);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Item/Items")]
    public async Task<ActionResult<List<Item>>> GetItems()
    {
        try
        {
            return Ok(await _itemServices.GetItems());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Item/{id}")]
    public async Task<ActionResult<Item>> GetItem(Guid id)
    {
        try
        {
            var result = await _itemServices.GetItem(id);

            if (result == null)
            {
                return NotFound($"Item with Id = {id} not found");
            }

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpDelete("Item/{id}")]
    public async Task<ActionResult<Item>> DeleteItem(Guid id)
    {
        try
        {
            var itemToDelete = await _itemServices.GetItem(id);

            if (itemToDelete == null)
            {
                return NotFound($"Item with Id = {id} not found");
            }

            return await _itemServices.DeleteItem(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("Item/{id}")]
    public async Task<ActionResult<Item>> UpdateItem(Guid id, Item item)
    {
        try
        {
            if(id != item.Id)
            {
                return BadRequest("Item ID mismatch");
            }

            var itemToUpdate = await _itemServices.GetItem(id);

            if(itemToUpdate == null)
            {
                return NotFound($"Item with Id = {id} not found");
            }

            return await _itemServices.UpdateItem(item);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
 
}
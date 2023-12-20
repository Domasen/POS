﻿using API.OrdersComponent.Models;
using API.OrdersComponent.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.OrdersComponent.Controllers;

public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderRepository _orderRepository;
    public OrdersController(ILogger<OrdersController> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }
    
    [HttpPost("Order")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Order>> CreateOrder([FromBody]Order? order)
    {
        try
        {
            if (order == null)
            {
                return BadRequest();
            }

            var createdOrder = await _orderRepository.AddOrder(order);

            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving dara from the database");
        }
    } 
    
    [HttpGet("Order/Orders")]
    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        try
        {
            return Ok(await _orderRepository.GetOrders());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Order/{id}")]
    public async Task<ActionResult<Order>> GetOrder(Guid id)
    {
        try
        {
            var result = await _orderRepository.GetOrder(id);

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

    [HttpDelete("Order/{id}")]
    public async Task<ActionResult<Order>> DeleteOrder(Guid id)
    {
        try
        {
            var orderToDelete = await _orderRepository.GetOrder(id);

            if (orderToDelete == null)
            {
                return NotFound($"Order with Id = {id} not found");
            }

            return await _orderRepository.DeleteOrder(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("Order/{id}")]
    public async Task<ActionResult<Order>> UpdateOrder(Guid id, [FromBody]Order order)
    {
        try
        {
            if(id != order.Id)
            {
                return BadRequest("Order ID mismatch");
            }

            var orderToUpdate = await _orderRepository.GetOrder(id);

            if(orderToUpdate == null)
            {
                return NotFound($"Order with Id = {id} not found");
            }

            return await _orderRepository.UpdateOrder(order);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
}
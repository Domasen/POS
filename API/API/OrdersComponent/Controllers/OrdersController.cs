using API.OrdersComponent.Models;
using API.OrdersComponent.Services;
using API.OrdersComponent.Sevices;
using Microsoft.AspNetCore.Mvc;

namespace API.OrdersComponent.Controllers;

public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderServices _orderServices;
    private readonly IOrderItemServices _orderItemServices;
    public OrdersController(ILogger<OrdersController> logger, IOrderServices orderServices, IOrderItemServices orderItemServices)
    {
        _logger = logger;
        _orderServices = orderServices;
        _orderItemServices = orderItemServices;
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

            var createdOrder = await _orderServices.AddOrder(order);

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
            return Ok(await _orderServices.GetOrders());
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
            var result = await _orderServices.GetOrder(id);

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
            var orderToDelete = await _orderServices.GetOrder(id);

            if (orderToDelete == null)
            {
                return NotFound($"Order with Id = {id} not found");
            }

            return await _orderServices.DeleteOrder(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    //Validacija i atskira metoda
    [HttpPut("Order/{id}")]
    public async Task<ActionResult<Order>> UpdateOrder(Guid id, [FromBody]Order order)
    {
        try
        {
            if(id != order.Id)
            {
                return BadRequest("Order ID mismatch");
            }

            var orderToUpdate = await _orderServices.GetOrder(id);

            if(orderToUpdate == null)
            {
                return NotFound($"Order with Id = {id} not found");
            }
                //daryti servisa kuris zino apie repozitorija controleri nubutu repositorijos
            return await _orderServices.UpdateOrder(order);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
    
    //////////OrderItem
    
    [HttpPost("OrderItem")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<OrderItem>> CreateOrderItem([FromBody]OrderItem? orderItem)
    {
        try
        {
            if (orderItem == null)
            {
                return BadRequest();
            }

            var createdOrderItem = await _orderItemServices.AddOrderItem(orderItem);

            return CreatedAtAction(nameof(GetOrderItem), new { id = createdOrderItem.Id }, createdOrderItem);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving dara from the database");
        }
    } 
    
    [HttpGet("OrderItem/OrderItems")]
    public async Task<ActionResult<List<OrderItem>>> GetOrderItems()
    {
        try
        {
            return Ok(await _orderItemServices.GetOrderItems());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("OrderItem/{id}")]
    public async Task<ActionResult<OrderItem>> GetOrderItem(Guid id)
    {
        try
        {
            var result = await _orderItemServices.GetOrderItem(id);

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

    [HttpDelete("OrderItem/{id}")]
    public async Task<ActionResult<OrderItem>> DeleteOrderItem(Guid id)
    {
        try
        {
            var orderItemToDelete = await _orderItemServices.GetOrderItem(id);

            if (orderItemToDelete == null)
            {
                return NotFound($"OrderItem with Id = {id} not found");
            }

            return await _orderItemServices.DeleteOrderItem(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("OrderItem/{id}")]
    public async Task<ActionResult<OrderItem>> UpdateOrderItem(Guid id, [FromBody]OrderItem orderItem)
    {
        try
        {
            if(id != orderItem.Id)
            {
                return BadRequest("OrderItem ID mismatch");
            }

            var orderItemToUpdate = await _orderItemServices.GetOrderItem(id);

            if(orderItemToUpdate == null)
            {
                return NotFound($"OrderItem with Id = {id} not found");
            }

            return await _orderItemServices.UpdateOrderItem(orderItem);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
}
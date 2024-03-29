﻿using API.Data;
using API.PaymentComponent.Models;
using API.PaymentComponent.Repository;
using API.PaymentComponent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.PaymentComponent.Controllers;
// https://www.pragimtech.com/blog/blazor/delete-in-asp.net-core-rest-api/
[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly ILogger<PaymentController> _logger;
    private readonly IPaymentServices _paymentServices;
    private readonly IPaymentMethodServices _paymentMethodServices;
    public PaymentController(ILogger<PaymentController> logger, IPaymentServices paymentServices, IPaymentMethodServices paymentMethodServices)
    {
        _logger = logger;
        _paymentServices = paymentServices;
        _paymentMethodServices = paymentMethodServices;
    }
    
    [HttpPost("Payment")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Payment>> CreatePayment(PaymentDto? payment)
    {
        try
        {
            if (payment == null)
            {
                return BadRequest();
            }

            var createdPayment = await _paymentServices.AddPayment(payment);

            return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.Id }, createdPayment);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving dara from the database");
        }
    } 
    
    [HttpGet("Payment/Payments")]
    public async Task<ActionResult<List<Payment>>> GetPayments()
    {
        try
        {
            return Ok(await _paymentServices.GetPayments());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("Payment/{id}")]
    public async Task<ActionResult<Payment>> GetPayment(Guid id)
    {
        try
        {
            var result = await _paymentServices.GetPayment(id);

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

    [HttpDelete("Payment/{id}")]
    public async Task<ActionResult<Payment>> DeletePayment(Guid id)
    {
        try
        {
            var paymentToDelete = await _paymentServices.GetPayment(id);

            if (paymentToDelete == null)
            {
                return NotFound($"Payment with Id = {id} not found");
            }

            return await _paymentServices.DeletePayment(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("Payment/{id}")]
    public async Task<ActionResult<Payment>> UpdatePayment(Guid id, Payment payment)
    {
        try
        {
            if(id != payment.Id)
            {
                return BadRequest("Payment ID mismatch");
            }

            var paymentToUpdate = await _paymentServices.GetPayment(id);

            if(paymentToUpdate == null)
            {
                return NotFound($"Payment with Id = {id} not found");
            }

            return await _paymentServices.UpdatePayment(payment);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
     //PaymentMethod 
     
     [HttpPost("PaymentMethod")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<PaymentMethod>> CreatePaymentMethod(PaymentMethodDto? paymentMethod)
    {
        try
        {
            if (paymentMethod == null)
            {
                return BadRequest();
            }

            var createdPaymentMethod = await _paymentMethodServices.AddPaymentMethod(paymentMethod);

            return CreatedAtAction(nameof(GetPaymentMethod), new { id = createdPaymentMethod.Id }, createdPaymentMethod);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving dara from the database");
        }
    } 
    
    [HttpGet("PaymentMethod/PaymentMethods")]
    public async Task<ActionResult<List<PaymentMethod>>> GetPaymentMethods()
    {
        try
        {
            return Ok(await _paymentMethodServices.GetPaymentMethods());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }
    
    [HttpGet("PaymentMethod/{id}")]
    public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(Guid id)
    {
        try
        {
            var result = await _paymentMethodServices.GetPaymentMethod(id);

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

    [HttpDelete("PaymentMethod/{id}")]
    public async Task<ActionResult<PaymentMethod>> DeletePaymentMethod(Guid id)
    {
        try
        {
            var paymentMethodToDelete = await _paymentMethodServices.GetPaymentMethod(id);

            if (paymentMethodToDelete == null)
            {
                return NotFound($"Payment Method with Id = {id} not found");
            }

            return await _paymentMethodServices.DeletePaymentMethod(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }
    
    [HttpPut("PaymentMethod/{id}")]
    public async Task<ActionResult<PaymentMethod>> UpdatePaymentMethod(Guid id, PaymentMethod paymentMethod)
    {
        try
        {
            if(id != paymentMethod.Id)
            {
                return BadRequest("Payment Method  ID mismatch");
            }

            var paymentMethodToUpdate = await _paymentMethodServices.GetPaymentMethod(id);

            if(paymentMethodToUpdate == null)
            {
                return NotFound($"Payment Method with Id = {id} not found");
            }

            return await _paymentMethodServices.UpdatePaymentMethod(paymentMethod);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }

}
using API.Data;
using API.PaymentComponent.Models;
using API.PaymentComponent.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.PaymentComponent.Controllers;
// https://www.pragimtech.com/blog/blazor/delete-in-asp.net-core-rest-api/
[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly ILogger<PaymentController> _logger;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    public PaymentController(ILogger<PaymentController> logger, IPaymentRepository paymentRepository, IPaymentMethodRepository paymentMethodRepository)
    {
        _logger = logger;
        _paymentRepository = paymentRepository;
        _paymentMethodRepository = paymentMethodRepository;
    }
    
    [HttpPost("Payment")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Payment>> CreatePayment(Payment? payment)
    {
        try
        {
            if (payment == null)
            {
                return BadRequest();
            }

            var createdPayment = await _paymentRepository.AddPayment(payment);

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
            return Ok(await _paymentRepository.GetPayments());
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
            var result = await _paymentRepository.GetPayment(id);

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
            var paymentToDelete = await _paymentRepository.GetPayment(id);

            if (paymentToDelete == null)
            {
                return NotFound($"Payment with Id = {id} not found");
            }

            return await _paymentRepository.DeletePayment(id);
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

            var paymentToUpdate = await _paymentRepository.GetPayment(id);

            if(paymentToUpdate == null)
            {
                return NotFound($"Payment with Id = {id} not found");
            }

            return await _paymentRepository.UpdatePayment(payment);
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
    public async Task<ActionResult<PaymentMethod>> CreatePaymentMethod(PaymentMethod? paymentMethod)
    {
        try
        {
            if (paymentMethod == null)
            {
                return BadRequest();
            }

            var createdPaymentMethod = await _paymentMethodRepository.AddPaymentMethod(paymentMethod);

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
            return Ok(await _paymentMethodRepository.GetPaymentMethods());
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
            var result = await _paymentMethodRepository.GetPaymentMethod(id);

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
            var paymentMethodToDelete = await _paymentMethodRepository.GetPaymentMethod(id);

            if (paymentMethodToDelete == null)
            {
                return NotFound($"Payment Method with Id = {id} not found");
            }

            return await _paymentMethodRepository.DeletePaymentMethod(id);
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

            var paymentMethodToUpdate = await _paymentMethodRepository.GetPaymentMethod(id);

            if(paymentMethodToUpdate == null)
            {
                return NotFound($"Payment Method with Id = {id} not found");
            }

            return await _paymentMethodRepository.UpdatePaymentMethod(paymentMethod);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }

}
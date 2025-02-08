using Contract.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IServiceManager serviceManager, ILogger<PaymentController> logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetPaymentsByUserId(long userId)
    {
        var payments = await _serviceManager.PaymentService.GetAllByUserIdAsync(userId);
        return Ok(payments);
    }

    [Authorize]
    [HttpGet("{paymentId}/details")]
    public async Task<IActionResult> GetPaymentDetails(long paymentId)
    {
        var paymentDetails = await _serviceManager.PaymentService.GetByIdAsync(paymentId);
        return Ok(paymentDetails);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentForCreationDto paymentDto)
    {
        await _serviceManager.PaymentService.CreateAsync(paymentDto);
        return Created(string.Empty, null);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{paymentId}")]
    public async Task<IActionResult> UpdatePayment(long paymentId, [FromBody] PaymentForUpdateDto paymentDto)
    {
        await _serviceManager.PaymentService.UpdateAsync(paymentId, paymentDto);
        return NoContent();
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{paymentId}")]
    public async Task<IActionResult> DeletePayment(long paymentId)
    {
        await _serviceManager.PaymentService.DeleteAsync(paymentId);
        return NoContent();
    }
}

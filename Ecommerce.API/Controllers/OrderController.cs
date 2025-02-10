using Contract.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IServiceManager serviceManager, ILogger<OrderController> logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        try
        {
            var orders = await _serviceManager.OrderService.GetAll();
            return Ok(orders);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while getting orders: {e.Message}");
            throw;
        }
    }

    [Authorize]
    [HttpGet("{id}/details")]
    public async Task<IActionResult> GetOrderDetails(long id)
    {
        try
        {
            var orderDetails = await _serviceManager.OrderService.GetOrderDetailsAsync(id);
            return Ok(orderDetails);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while getting order details: {e.Message}");
            throw;
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderForCreationDto orderDto)
    {
        try
        {
            var createdOrder = await _serviceManager.OrderService.CreateAsync(orderDto);
            return Created(string.Empty, createdOrder);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while creating an order: {e.Message}");
            throw;
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(long id, [FromBody] OrderForUpdateDto orderDto)
    {
        try
        {
            await _serviceManager.OrderService.UpdateAsync(id, orderDto);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while updating an order: {e.Message}");
            throw;
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(long id)
    {
        try
        {
            await _serviceManager.OrderService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while deleting an order: {e.Message}");
            throw;
        }
    }
}

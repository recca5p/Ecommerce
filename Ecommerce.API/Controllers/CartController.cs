using Contract.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILogger<CartController> _logger;

    public CartController(IServiceManager serviceManager, ILogger<CartController> logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(long userId)
    {
        try
        {
            var cart = await _serviceManager.CartService.GetByUserIdAsync(userId);
            return Ok(cart);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while retrieving the cart: {e.Message}");
            throw;
        }
    }

    [Authorize]
    [HttpPost("{userId}")]
    public async Task<IActionResult> AddItemToCart(long userId, [FromBody] CartItemForCreationDto cartItemDto)
    {
        try
        {
            await _serviceManager.CartService.AddItemAsync(userId, cartItemDto);
            return Created(string.Empty, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while adding an item to the cart: {e.Message}");
            throw;
        }
    }

    [Authorize]
    [HttpPut("{userId}/update-item/{itemId}")]
    public async Task<IActionResult> UpdateCartItem(long userId, long itemId, [FromBody] CartItemForUpdateDto cartItemDto)
    {
        try
        {
            await _serviceManager.CartService.UpdateItemAsync(userId, itemId, cartItemDto);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while updating a cart item: {e.Message}");
            throw;
        }
    }

    [Authorize]
    [HttpDelete("{userId}/remove-item/{itemId}")]
    public async Task<IActionResult> RemoveCartItem(long userId, long itemId)
    {
        try
        {
            await _serviceManager.CartService.RemoveItemAsync(userId, itemId);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while removing a cart item: {e.Message}");
            throw;
        }
    }
}

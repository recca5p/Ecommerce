using Contract.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductVariantController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILogger<ProductVariantController> _logger;

    public ProductVariantController(IServiceManager serviceManager, ILogger<ProductVariantController> logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetVariantsByProductId(long productId)
    {
        try
        {
            var variants = await _serviceManager.ProductVariantService.GetAllByProductIdAsync(productId);
            return Ok(variants);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while getting product variants: {e.Message}");
            throw;
        }
    }

    [AllowAnonymous]
    [HttpGet("variant/{id}")]
    public async Task<IActionResult> GetVariantById(long id)
    {
        try
        {
            var variant = await _serviceManager.ProductVariantService.GetByIdAsync(id);
            return Ok(variant);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while getting a product variant: {e.Message}");
            throw;
        }
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("{productId}")]
    public async Task<IActionResult> CreateVariant(long productId, [FromBody] ProductVariantForCreationDto variantDto)
    {
        try
        {
            await _serviceManager.ProductVariantService.CreateAsync(variantDto, productId);
            return Created(string.Empty, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while creating a product variant: {e.Message}");
            throw;
        }
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVariant(long id, [FromBody] ProductVariantForUpdateDto variantDto)
    {
        try
        {
            await _serviceManager.ProductVariantService.UpdateAsync(id, variantDto);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while updating a product variant: {e.Message}");
            throw;
        }
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVariant(long id)
    {
        try
        {
            await _serviceManager.ProductVariantService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while deleting a product variant: {e.Message}");
            throw;
        }
    }
}

using Contract.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILogger<ImageController> _logger;

    public ImageController(IServiceManager serviceManager, ILogger<ImageController> logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetImagesByProductId(long productId)
    {
        try
        {
            var images = await _serviceManager.ImageService.GetAllByProductIdAsync(productId);
            return Ok(images);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while getting images: {e.Message}");
            throw;
        }
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> AddImage(long productId, [FromBody] ImageForCreationDto imageDto)
    {
        try
        {
            await _serviceManager.ImageService.CreateAsync(imageDto, productId);
            return Created(string.Empty, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while adding an image: {e.Message}");
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImage(long id)
    {
        try
        {
            await _serviceManager.ImageService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while deleting an image: {e.Message}");
            throw;
        }
    }
}

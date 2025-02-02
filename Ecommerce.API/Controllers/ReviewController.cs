using Contract.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(IServiceManager serviceManager, ILogger<ReviewController> logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetReviewsByProductId(long productId)
    {
        var reviews = await _serviceManager.ReviewService.GetAllByProductIdAsync(productId);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] ReviewForCreationDto reviewDto)
    {
        await _serviceManager.ReviewService.CreateAsync(reviewDto);
        return Created(string.Empty, null);
    }
}

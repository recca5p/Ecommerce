using Contract.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(IServiceManager serviceManager, ILogger<CategoryController> logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var categories = await _serviceManager.CategoryService.GetAllAsync();
            return Ok(categories);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while getting categories: {e.Message}");
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(long ID)
    {
        try
        {
            var category = await _serviceManager.CategoryService.GetByIdAsync(ID);
            return Ok(category);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while getting a category: {e.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryForCreationDto categoryForCreation)
    {
        try
        {
            await _serviceManager.CategoryService.CreateAsync(categoryForCreation);
            return Created(string.Empty, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while creating a category: {e.Message}");
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long ID, CategoryForUpdateDto categoryForUpdate)
    {
        try
        {
            await _serviceManager.CategoryService.UpdateAsync(ID, categoryForUpdate);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while updating a category: {e.Message}");
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long ID)
    {
        try
        {
            await _serviceManager.CategoryService.DeleteAsync(ID);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while deleting a category: {e.Message}");
            throw;
        }
    }
}

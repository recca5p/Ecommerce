using Contract.DTOs.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILogger<UserController> _logger;

    public UserController(IServiceManager serviceManager, ILogger<UserController> logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        try
        {
            var users = await _serviceManager.UserService.GetAllAsync();
            return Ok(users);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while getting all users: {e.Message}");
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(long id)
    {
        try
        {
            var user = await _serviceManager.UserService.GetByIdAsync(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while getting a user: {e.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(UserForCreationDto userForCreation)
    {
        try
        {
            // Call the service to create the user
            var createdUser = await _serviceManager.UserService.CreateAsync(userForCreation);

            // Return the newly created user's id and details
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while creating a user: {e.Message}");
            throw;
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<User>> Update(long id, UserForUpdateDto userForUpdate)
    {
        try
        {
            await _serviceManager.UserService.UpdateAsync(id, userForUpdate);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while updating a user: {e.Message}");
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> Delete(long id)
    {
        try
        {
            await _serviceManager.UserService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while deleting a user: {e.Message}");
            throw;
        }
    }
}

using Contract.DTOs.Request;
using Contract.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public AuthController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _serviceManager.AuthService.Login(request.Email, request.Password);
        if (result == null)
            return Unauthorized(new { message = "Invalid credentials" });

        return Ok(new
        {
            accessToken = result.AccessToken,
            refreshToken = result.RefreshToken
        });
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequest request)
    {
        var newToken = await _serviceManager.AuthService.RefreshToken(request.Token);
        if (newToken == null)
            return Unauthorized(new { message = "Invalid token" });

        return Ok(new { accessToken = newToken });
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var success = await _serviceManager.AuthService.RegisterUser(request.Name, request.Email, request.Password, false, null);

        if (!success)
            return BadRequest(new { message = "User already exists or registration failed" });

        return Ok(new { message = "User registered successfully" });
    }
    
    [HttpGet]
    [Route("api/profile")]
    public IActionResult GetProfile()
    {
        var claims = TokenExtension.GetTokenClaims();

        if (claims == null)
        {
            return Unauthorized("Invalid or expired token");
        }

        var profile = new
        {
            UserId = claims.ContainsKey("userId") ? claims["userId"]?.ToString() : null,
            Name = claims.ContainsKey("name") ? claims["name"]?.ToString() : null,
            Email = claims.ContainsKey("email") ? claims["email"]?.ToString() : null,
            IsAdmin = claims.ContainsKey("isAdmin") ? (bool)claims["isAdmin"] : false
        };

        return Ok(profile);
    }
}
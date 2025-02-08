using Contract.Models;
using Domain.Entities;
using Paseto.Authentication;

namespace Services.Abstraction;

public interface IAuthService
{
    Task<AuthResult?> Login(string email, string password);
    Task<bool> ValidateToken(string token);
    Task<AuthResult?> RefreshToken(string token);
    Task<bool> RegisterUser(string name, string email, string password, bool isAdmin, string token);
}
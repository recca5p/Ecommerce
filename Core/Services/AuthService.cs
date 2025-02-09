using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Contract.Models;
using Domain.Entities;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;
using Paseto.Authentication;
using Services.Abstraction;

public class AuthService : IAuthService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly byte[] _symmetricKey;

    public AuthService(IRepositoryManager repositoryManager, string secretKeyBase64)
    {
        _repositoryManager = repositoryManager;
        _symmetricKey = Convert.FromBase64String(secretKeyBase64);
    }

    public async Task<AuthResult?> Login(string email, string password)
    {
        var user = await _repositoryManager.UserRepository.GetByEmailAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        return new AuthResult
        {
            AccessToken = TokenExtension.GenerateAccessToken(user),
            RefreshToken = TokenExtension.GenerateRefreshToken(user)
        };
    }

    public async Task<AuthResult?> RefreshToken(string token)
    {
        var isValid = TokenExtension.ValidateToken(token);
        if (!isValid) return null;

        var payload = TokenExtension.GetTokenClaims();
        var email = payload?["email"]?.ToString();

        if (string.IsNullOrEmpty(email))
            return null;

        var user = await _repositoryManager.UserRepository.GetByEmailAsync(email);
        if (user == null)
            return null;

        return new AuthResult
        {
            AccessToken = TokenExtension.GenerateAccessToken(user),
            RefreshToken = TokenExtension.GenerateRefreshToken(user)
        };
    }

    public async Task<bool> ValidateToken(string token)
    {
        return TokenExtension.ValidateToken(token);
    }

    public async Task<bool> RegisterUser(string name, string email, string password, bool isAdmin, string token)
    {
        if (await _repositoryManager.UserRepository.GetByEmailAsync(email) != null)
        {
            return false;
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var newUser = new User
        {
            Name = name,
            Email = email,
            Password = hashedPassword,
            IsAdmin = isAdmin,
            CreatedById = 0
        };

        _repositoryManager.UserRepository.Insert(newUser);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
        return true;
    }
}

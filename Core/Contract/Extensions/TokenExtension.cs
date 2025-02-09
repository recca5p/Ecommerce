using System;
using System.Collections.Generic;
using System.Threading;
using Domain.Entities;
using Paseto.Authentication;

public static class TokenExtension
{
    private static byte[] _secretKey;
    private static readonly AsyncLocal<string> _token = new();

    public static void Configure(string secretKey)
    {
        _secretKey = Convert.FromBase64String(secretKey);
    }

    public static void SetToken(string token)
    {
        _token.Value = token;
    }

    public static string GetToken()
    {
        return _token.Value;
    }

    public static string GenerateAccessToken(User user)
    {
        var now = DateTime.UtcNow;

        var claims = new PasetoInstance
        {
            Subject = user.UserId.ToString(),
            Expiration = now.AddMinutes(30),
            IssuedAt = now,
            AdditionalClaims = new Dictionary<string, object>
            {
                { "email", user.Email },
                { "name", user.Name },
                { "isAdmin", user.IsAdmin },
                { "userId", user.UserId },
            }
        };

        return PasetoUtility.Encrypt(_secretKey, claims);
    }

    public static string GenerateRefreshToken(User user)
    {
        var now = DateTime.UtcNow;

        var claims = new PasetoInstance
        {
            Subject = user.UserId.ToString(),
            Expiration = DateTime.UtcNow.Date.AddDays(1).AddTicks(-1),
            IssuedAt = now
        };

        return PasetoUtility.Encrypt(_secretKey, claims);
    }

    public static bool ValidateToken(string token)
    {
        try
        {
            var parsedToken = PasetoUtility.Decrypt(_secretKey, token, validateTimes: true);
            return parsedToken != null;
        }
        catch
        {
            return false;
        }
    }

    public static Dictionary<string, object> GetTokenClaims()
    {
        if (string.IsNullOrEmpty(_token.Value))
            return null;

        try
        {
            var pasetoInstance = PasetoUtility.Decrypt(_secretKey, _token.Value, validateTimes: true);
            if (pasetoInstance == null)
                return null;

            var claims = new Dictionary<string, object>
            {
                { "subject", pasetoInstance.Subject },
                { "expiration", pasetoInstance.Expiration },
                { "issuedAt", pasetoInstance.IssuedAt },
            };

            if (pasetoInstance.AdditionalClaims != null)
            {
                foreach (var claim in pasetoInstance.AdditionalClaims)
                {
                    claims[claim.Key] = claim.Value;
                }
            }

            return claims;
        }
        catch
        {
            return null;
        }
    }

    public static string GetClaimValue(string claimType)
    {
        var claims = GetTokenClaims();
        if (claims == null || !claims.ContainsKey(claimType))
            return null;

        return claims[claimType]?.ToString();
    }

    public static string GetUserId()
    {
        return GetClaimValue("userId");
    }
}

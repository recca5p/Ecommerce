using System.Text;
using Domain.Entities;
using Paseto.Authentication;

public static class TokenExtension
{
    private static byte[] _secretKey;

    public static void Configure(string secretKey)
    {
        _secretKey = Encoding.UTF8.GetBytes(secretKey);
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
                { "isAdmin", user.IsAdmin }
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

    public static Dictionary<string, object> GetTokenClaims(string token)
    {
        try
        {
            var pasetoInstance = PasetoUtility.Decrypt(_secretKey, token, validateTimes: true);
            if (pasetoInstance == null)
                return null;

            var claims = new Dictionary<string, object>
            {
                { "subject", pasetoInstance.Subject },
                { "expiration", pasetoInstance.Expiration },
                { "issuedAt", pasetoInstance.IssuedAt }
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

    public static string GetClaimValue(string token, string claimType)
    {
        var claims = GetTokenClaims(token);
        if (claims == null || !claims.ContainsKey(claimType))
            return null;

        return claims[claimType]?.ToString();
    }
}
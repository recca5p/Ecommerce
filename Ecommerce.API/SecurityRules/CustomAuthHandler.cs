using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Ecommerce.API.SecurityRules;

public class CustomAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public CustomAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder) { }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return AuthenticateResult.Fail("Missing Token");
        }
        
        TokenExtension.SetToken(token);

        var claims = TokenExtension.GetTokenClaims();
        if (claims == null)
        {
            return AuthenticateResult.Fail("Invalid Token");
        }

        var identity = new ClaimsIdentity(claims.Select(c => new Claim(c.Key, c.Value.ToString()!)), Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
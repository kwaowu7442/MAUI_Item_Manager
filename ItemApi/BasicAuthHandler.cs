using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var header))
            return Task.FromResult(AuthenticateResult.Fail("Missing header"));
        try
        {
            var encoded = header.ToString().Replace("Basic ", "");
            var parts = Encoding.UTF8.GetString(Convert.FromBase64String(encoded)).Split(':', 2);
            if (parts[0] == "Owusu01" && parts[1] == "Password1")
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, parts[0]) }, Scheme.Name);
                var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
        }
        catch { }
        return Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
    }
}
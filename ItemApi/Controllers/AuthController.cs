using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [Authorize(AuthenticationSchemes = "Basic")]
    [HttpGet("login")]
    public IActionResult Login() => Ok();
}
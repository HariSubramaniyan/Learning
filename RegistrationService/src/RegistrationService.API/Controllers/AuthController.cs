using Microsoft.AspNetCore.Mvc;
using RegistrationService.Application.Abstractions.Authentication;

namespace RegistrationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthController(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("token")]
    public ActionResult<string> GenerateToken([FromQuery] string subject = "registration-service")
    {
        var token = _jwtTokenGenerator.GenerateServiceToken(subject, ["ServiceClient"]);
        return Ok(token);
    }
}

using EcommVendor.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcommVendor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Username and password are required.");
        }

        // Simple demo auth.
        return Ok(new LoginResponse
        {
            Username = request.Username,
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
        });
    }
}

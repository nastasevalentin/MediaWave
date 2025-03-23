using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MW.Application.Contracts.Identity;
using MW.Application.Contracts.Interfaces;
using MW.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace MW.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }

            var (status, token) = await _authService.Login(model);

            _logger.LogInformation("Token: " + token);

            
            if (status == 0)
            {
                return BadRequest(token); // token holds error message in this case
            }

            Response.Cookies.Append("Authorization", $"Bearer {token}", new CookieOptions
            {
                HttpOnly = true,        
                Secure = true,          
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1) 
            });

            return Ok(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }
                    
            var (status, message) = await _authService.Registration(model);

            if (status == 0)
            {
                return BadRequest(message);
            }

            return CreatedAtAction(nameof(Register), model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("Authorization"); // 👈 removes the JWT cookie
        return Redirect("/"); // or wherever you want
    }

    
    
    
    
    [HttpGet("test-auth")]
    [Authorize]
    public IActionResult TestAuth()
    {
        return Ok("Token is valid!");
    }

}
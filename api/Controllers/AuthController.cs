using api.DTOs;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _authService.Login(request);
        return result != null 
            ? Ok(result)
            : Unauthorized("Please check your email and password.");
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Login([FromBody] RegisterRequestDto request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _authService.Register(request);
        return result != null 
            ? Ok(result)
            : Unauthorized("Please check your email and password! maybe you already have a user with this email?");
    }
}
using ClothesShop.Application.DTOs;
using ClothesShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    ///     Login with email and password
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        var token = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
        if (token == null) return Unauthorized("Invalid email or password.");

        var response = new AuthResponseDto
        {
            Token = token,
            Email = loginDto.Email,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };

        return Ok(response);
    }

    /// <summary>
    ///     Register a new user
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
    {
        var success = await _authService.RegisterAsync(
            registerDto.Email,
            registerDto.Password,
            registerDto.FullName,
            registerDto.Role);

        if (!success) return BadRequest("Registration failed.");

        // Auto-login after registration
        var token = await _authService.LoginAsync(registerDto.Email, registerDto.Password);
        if (token == null) return BadRequest("Registration succeeded but login failed.");

        var response = new AuthResponseDto
        {
            Token = token,
            Email = registerDto.Email,
            FullName = registerDto.FullName,
            Role = registerDto.Role,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };

        return Ok(response);
    }
}
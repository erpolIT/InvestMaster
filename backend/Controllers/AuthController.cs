using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Database;
using backend.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly TokenProvider _tokenProvider;

    public AuthController(UserManager<User> userManager, TokenProvider tokenProvider, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
        _tokenProvider = tokenProvider;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return Unauthorized();
        }

        var token = _tokenProvider.Create(user);
        
        return Ok(new { Token = token, Message = "Data received successfully" });
    }
    
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User
        {
            Email = registerDto.Email,
            UserName = registerDto.Email        
        };
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { Message = "User registered successfully" });
    }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    
    public string ConfirmPassword { get; set; }
}
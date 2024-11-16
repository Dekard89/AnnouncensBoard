using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AnnouncensBoard.BLL.DTO;
using AnnouncensBoard.Options;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnnouncensBoard.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<UsersController> _logger;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<RegisterRequest> _registerValidator;
    private readonly JwtOptions _options;

    public UsersController(UserManager<IdentityUser> userManager,
        ILogger<UsersController> logger,
        IValidator<LoginRequest> loginValidator,
        IValidator<RegisterRequest> registerValidator,
        JwtOptions jwtOptions)
    {

        _userManager = userManager;
        _logger = logger;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
        _options = jwtOptions;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _loginValidator.ValidateAsync(loginRequest);
        if (!result.IsValid)
            return BadRequest(result.Errors);
        else
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                _logger.LogInformation($"User {user.Email} logged in.");
                var claims = await _userManager.GetClaimsAsync(user);
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                var token = new JwtSecurityToken(
                   issuer: _options.Issuer,
                   audience: _options.Audience,
                   expires: _options.TokenLifetime,
                   signingCredentials: new SigningCredentials(_options.GetSymmetricSecurityKey(),
                       SecurityAlgorithms.HmacSha256Signature),
                   claims: claims);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenString);
            }
        }

        return Unauthorized();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var result = await _registerValidator.ValidateAsync(registerRequest);
        if (!result.IsValid)
            return BadRequest(result.Errors);
        
        var user = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (user != null)
            return BadRequest("Email already exists.");
        
        var newUser = new IdentityUser
        {
            Email = registerRequest.Email,
            Id = new Guid().ToString(),
            UserName = registerRequest.Username,
            PhoneNumber = registerRequest.Phone

        };
        var resultUser = await _userManager.CreateAsync(newUser, registerRequest.Password);
        if (resultUser.Succeeded)
            return BadRequest(resultUser.Errors);
        
       
        await _userManager.AddClaimAsync(newUser, new Claim("DateOfBirth", registerRequest.Birthday.ToString()));
        return Ok();
    }

    

}
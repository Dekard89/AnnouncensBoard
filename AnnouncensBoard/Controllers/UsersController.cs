using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AnnouncensBoard.BLL.DTO;
using AnnouncensBoard.Options;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    private readonly IOptions<JwtOptions> _options;

    public UsersController(UserManager<IdentityUser> userManager,
        ILogger<UsersController> logger,
        IValidator<LoginRequest> loginValidator,
        IValidator<RegisterRequest> registerValidator,
        IOptions<JwtOptions> jwtOptions)
    {

        _userManager = userManager;
        _logger = logger;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
        _options = jwtOptions;
    }
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _loginValidator.ValidateAsync(loginRequest);
        if (!result.IsValid)
            return BadRequest(result.Errors);
        
        
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if(user==null)
            return NotFound("user is not register");

        var checkResult= await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if(!checkResult)
            return BadRequest("password invalid");
        var claims = await _userManager.GetClaimsAsync(user);
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        TimeSpan expire = new(0, 0, 0);
        var token = new JwtSecurityToken(
           issuer: _options.Value.Issuer,
           audience: _options.Value.Audience,
           expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_options.Value.TokenLifetime)),
           signingCredentials: new SigningCredentials(_options.Value.GetSymmetricSecurityKey(),
               SecurityAlgorithms.HmacSha256Signature),
           claims: claims); 

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(tokenString);
    }
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if (registerRequest is null)
            return BadRequest("Request is null");

        var result = await _registerValidator.ValidateAsync(registerRequest);
        if (!result.IsValid)
            return BadRequest(result.Errors);
        
        var user = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (user != null)
            return BadRequest("Email already exists.");
        
        var newUser = new IdentityUser
        {
            Email = registerRequest.Email,
            UserName = registerRequest.Username,
            PhoneNumber = registerRequest.Phone

        };
        var resultUser = await _userManager.CreateAsync(newUser, registerRequest.Password);
        if (!resultUser.Succeeded)
            return BadRequest(resultUser.Errors);
        
       
        await _userManager.AddClaimAsync(newUser, new Claim("DateOfBirth", registerRequest.Birthday));
        return Ok();
    }

    

}
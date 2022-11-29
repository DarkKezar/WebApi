using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.DTO;
using Service.Services.AccountService;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountDetailsController : Controller
{
    //some private readonly service
    private readonly IConfiguration _configuration;
    private readonly IAccountService _accountService;
    private readonly UserManager<User> _userManager;

    public AccountDetailsController(IConfiguration configuration, IAccountService accountService, UserManager<User> userManager)
    {
        _configuration = configuration;
        _accountService = accountService;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize]
    [Route("GetUser")]
    public async Task<ActionResult<UserShowModel>> GetUserDataAsync()
    {
        return await _accountService.GetUserDataAsync((await _userManager.GetUserAsync(null)));
    }

    [HttpPost]
    [Route("SignUp")]
    public async Task<ActionResult> SignUpAsync(UserCreationModel model)
    {
        return await _accountService.SignUpAsync(model);
    }
    
    [HttpPost]
    [Route("SignIn")]
    public async Task<IResult> SignInAsync(string email, string password)
    {
        if (await _userManager.CheckPasswordAsync((await _userManager.FindByEmailAsync(email)), password))
        {
            var issuer = _configuration.GetValue<string>("Jwt:Issuer");
            var audience = _configuration.GetValue<string>("Jwt:Audience");
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return Results.Ok(stringToken);
        }
        return Results.Unauthorized();
    }

    [HttpPatch]
    [Authorize]
    [Route("ChangePassword")]
    public async Task<IdentityResult> ChangePasswordAsync(UserUpdateModel model)
    {
        return await _accountService.ChangePasswordAsync(model, (await _userManager.GetUserAsync(null)));
    }
    [HttpPatch]
    [Authorize]
    [Route("ChangeEmail")]
    public async Task<ActionResult> ChangeEmailAsync(UserUpdateModel model)
    {
        return await _accountService.ChangeEmailAsync(model, (await _userManager.GetUserAsync(null)));
    }
    [HttpPatch]
    [Authorize]
    [Route("ChangePhoto")]
    public async Task<ActionResult> ChangePhotoAsync(UserUpdateModel model)
    {
        return await _accountService.ChangePhotoAsync(model, (await _userManager.GetUserAsync(null)));
    }
    [HttpPatch]
    [Authorize]
    [Route("ChangeUserName")]
    public async Task<ActionResult> ChangeUserNameAsync(UserUpdateModel model)
    {
        return await _accountService.ChangeUserNameAsync(model, (await _userManager.GetUserAsync(null)));
    }
}


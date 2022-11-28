using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.DTO;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountDetailsController : Controller
{
    //some private readonly service
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public AccountDetailsController(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("GetUser")]
    public async Task<UserShowModel> GetUserDataAsync()
    {
        return new UserShowModel();
    }

    [HttpPost]
    [Route("SignUp")]
    public async Task<ActionResult> SignUpAsync(UserCreationModel model)
    {
        return new OkResult();
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
    [Route("ChangePassword")]
    public async Task<ActionResult> ChangePasswordAsync(UserUpdateModel model)
    {
        return new OkResult();
    }
    [HttpPatch]
    [Route("ChangeEmail")]
    public async Task<ActionResult> ChangeEmailAsync(UserUpdateModel model)
    {
        return new OkResult();
    }
    [HttpPatch]
    [Route("ChangePhoto")]
    public async Task<ActionResult> ChangePhotoAsync(UserUpdateModel model)
    {
        return new OkResult();
    }
    [HttpPatch]
    [Route("ChangeUserName")]
    public async Task<ActionResult> ChangeUserNameAsync(UserUpdateModel model)
    {
        return new OkResult();
    }
}


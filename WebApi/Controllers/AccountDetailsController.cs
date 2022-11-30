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
using Service.Validators;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountDetailsController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IAccountService _accountService;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _context;

    public AccountDetailsController(IConfiguration configuration, IAccountService accountService, UserManager<User> userManager, IHttpContextAccessor context)
    {
        _configuration = configuration;
        _accountService = accountService;
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    [Authorize]
    [Route("GetUser")]
    public async Task<ActionResult<UserShowModel>> GetUserDataAsync()
    {
        Guid ID = Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _accountService.GetUserDataAsync(ID);
    }

    [HttpPost]
    [Route("SignUp")]
    public async Task<ActionResult> SignUpAsync(UserCreationModel model)
    {
        UserCMValidator validator = new UserCMValidator();
        if (validator.Validate(model).IsValid)
            return await _accountService.SignUpAsync(model);
        else return new BadRequestResult();
    }
    
    [HttpPost]
    [Route("SignIn")]
    public async Task<ActionResult> SignInAsync(string email, string password)
    {
        if(await _userManager.CheckPasswordAsync((await _userManager.FindByEmailAsync(email)), password))
        {
            var issuer = _configuration.GetValue<string>("Jwt:Issuer");
            var audience = _configuration.GetValue<string>("Jwt:Audience");
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
            string ID = (await _userManager.FindByEmailAsync(email)).Id.ToString();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", ID),
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim(JwtRegisteredClaimNames.Jti,  ID)
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
            return new OkObjectResult(stringToken);
        }

        return new OkObjectResult(Results.Unauthorized());
    }

    [HttpPatch]
    [Authorize]
    [Route("ChangePassword")]
    public async Task<IdentityResult> ChangePasswordAsync(UserUpdateModel model)
    {
        Guid ID = Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _accountService.ChangePasswordAsync(model, ID);
    }
    [HttpPatch]
    [Authorize]
    [Route("ChangeEmail")] //rewrite
    public async Task<ActionResult> ChangeEmailAsync(UserUpdateModel model)
    {
        Guid ID = Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _accountService.ChangeEmailAsync(model, ID);
    }
    [HttpPatch]
    [Authorize]
    [Route("ChangePhoto")] //rewrite
    public async Task<ActionResult> ChangePhotoAsync(UserUpdateModel model)
    {
        Guid ID = Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _accountService.ChangePhotoAsync(model, ID);
    }
    [HttpPatch]
    [Authorize]
    [Route("ChangeUserName")] //rewrite
    public async Task<ActionResult> ChangeUserNameAsync(UserUpdateModel model)
    {
        Guid ID = Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _accountService.ChangeUserNameAsync(model, ID);
    }
}


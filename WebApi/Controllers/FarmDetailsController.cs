using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services.FarmService;

namespace WebApi.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FarmDetailsController : Controller
{
    private readonly IFarmService _farmService;
    private readonly UserManager<User> _userManager;

    public FarmDetailsController(IFarmService farmService, UserManager<User> userManager)
    {
        _farmService = farmService;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("Farm")]
    public async Task<ActionResult<Farm>> FarmInfoAsync(Guid Id)
    {
        return await _farmService.GetFarmInfoAsync(Id);
    }
    
    [HttpPost]
    [Route("AddUser")]
    public async Task<ActionResult> InviteFriendAsync(string email)
    {
        Guid ID =  Guid.Parse(this.HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _farmService.InviteFriendAsync(ID, email);
    }
}
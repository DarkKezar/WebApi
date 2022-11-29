using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.FarmsService;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FarmsOverwiewController : Controller
{
    private readonly IFarmsService _farmsService;
    private readonly UserManager<User> _userManager;

    public FarmsOverwiewController(IFarmsService farmsService, UserManager<User> userManager)
    {
        _farmsService = farmsService;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("MyFarm")]
    public async Task<ActionResult<Farm>> GetMyFarmAsync()
    {
        return await _farmsService.GetMyFarmAsync((await _userManager.GetUserAsync(null)));
    }

    [HttpGet]
    [Route("CollabFarms")]
    public async Task<ActionResult<List<Farm>>> GetCollabFarmsAsync()
    {
        return await _farmsService.GetCollabFarmsAsync((await _userManager.GetUserAsync(null)));
    }

    
    [HttpPost]
    [Route("CreateFarm")]
    public async Task<ActionResult> CreateNewFarmAsync(FarmCreationModel model)
    {
        return await _farmsService.CreateNewFarmAsync((await _userManager.GetUserAsync(null)), model);
    }
}
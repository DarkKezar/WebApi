using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.FarmsService;
using Service.Validators;

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
        Guid ID =  Guid.Parse(this.HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _farmsService.GetMyFarmAsync(ID);
    }

    [HttpGet]
    [Route("CollabFarms")]
    public async Task<ActionResult<List<Farm>>> GetCollabFarmsAsync()
    {
        Guid ID = Guid.Parse(this.HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _farmsService.GetCollabFarmsAsync(ID);
    }

    
    [HttpPost]
    [Route("CreateFarm")]
    public async Task<ActionResult> CreateNewFarmAsync(FarmCreationModel model)
    {
        FarmCMValidator validator = new FarmCMValidator();
        Guid ID = Guid.Parse(this.HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        if (validator.Validate(model).IsValid)
            return await _farmsService.CreateNewFarmAsync(ID, model);
        else return new BadRequestResult();
    }
}
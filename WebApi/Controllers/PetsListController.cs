using Core.Entities;
using Core.Repositories.PetRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services.PetsService;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PetsListController : Controller
{
    private readonly IPetsService _petsService;
    private readonly UserManager<User> _userManager;

    public PetsListController(IPetsService petsService, UserManager<User> userManager)
    {
        _petsService = petsService;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("GetPets")]
    public async Task<ActionResult<List<Pet>>> GetPetsAsync(Guid FarmId)
    {
        return await _petsService.GetPetsAsync(FarmId);
    }
    
    [HttpPatch]
    [Route("FeedPets")]
    public async Task<ActionResult> FeedPetsAsync(List<Guid> ids)
    {
        Guid ID =  Guid.Parse(this.HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _petsService.FeedPetsAsync(ID ,ids);
    }
    
    [HttpPatch]
    [Route("GetDrinkPets")]
    public async Task<ActionResult> GetDrinkPetsAsync(List<Guid> ids)
    {
        Guid ID =  Guid.Parse(this.HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _petsService.GetDrinkPetsAsync(ID ,ids);
    }
}
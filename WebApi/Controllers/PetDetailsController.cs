using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.PetService;

namespace WebApi.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PetDetailsController : Controller
{
    private readonly IPetService _petService;
    private readonly UserManager<User> _userManager;

    public PetDetailsController(IPetService petService, UserManager<User> userManager)
    {
        _petService = petService;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("Pet")]
    public async Task<ActionResult<Pet>> GetPetAsync(Guid id)
    {
        return await _petService.GetPetAsync(id);
    }
    
    [HttpPost]
    [Route("CreatePet")]
    public async Task<ActionResult> CreatePetAsync(PetCreationModel model)
    {
        return await _petService.CreatePetAsync((await _userManager.GetUserAsync(null)), model);
    }

    [HttpPatch]
    [Route("FeedPet")]
    public async Task<ActionResult> FeedPetAsync(Guid Id)
    {
        return await _petService.FeedPetAsync((await _userManager.GetUserAsync(null)), Id);
    }
    
    [HttpPatch]
    [Route("GetDrinkPet")]
    public async Task<ActionResult> GetDrinkPetAsync(Guid Id)
    {
        return await _petService.GetDrinkPetAsync((await _userManager.GetUserAsync(null)), Id);
    }
}
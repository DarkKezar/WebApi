using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.PetService;
using Service.Validators;

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
        PetCMValidator validator = new PetCMValidator();
        Guid ID =  Guid.Parse(this.HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

        if (validator.Validate(model).IsValid)
            return await _petService.CreatePetAsync(ID, model);
        else return new BadRequestObjectResult("Model is not correct");
    }

    [HttpPatch]
    [Route("FeedPet")]
    public async Task<ActionResult> FeedPetAsync(Guid Id)
    {
        Guid ID =  Guid.Parse(this.HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _petService.FeedPetAsync(ID, Id);
    }
    
    [HttpPatch]
    [Route("GetDrinkPet")]
    public async Task<ActionResult> GetDrinkPetAsync(Guid Id)
    {
        Guid ID =  Guid.Parse(this.HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        return await _petService.GetDrinkPetAsync(ID, Id);
    }
}
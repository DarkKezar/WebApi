using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PetDetailsController : Controller
{
    //some private readonly service

    [HttpGet]
    [Route("Pet")]
    public async Task<ActionResult<Pet>> GetPetAsync(Guid id)
    {
        return new Pet();
    }
    
    [HttpPost]
    [Route("CreatePet")]
    public async Task<ActionResult> CreatePetAsync(PetCreationModel model)
    {
        return new OkResult();
    }

    [HttpPatch]
    [Route("FeedPet")]
    public async Task<ActionResult> FeedPetAsync(Guid Id)
    {
        return new OkResult();
    }
    
    [HttpPatch]
    [Route("GetDrinkPet")]
    public async Task<ActionResult> GetDrinkPetAsync(Guid Id)
    {
        return new OkResult();
    }
}
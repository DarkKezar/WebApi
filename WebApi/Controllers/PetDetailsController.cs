using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PetDetailsController : Controller
{
    //some private readonly service

    [HttpGet]
    [Route("Pet")]
    public async Task<Pet> GetPet(Guid id)
    {
        return new Pet();
    }
    
    [HttpPost]
    [Route("CreatePet")]
    public async Task<ActionResult> CreatePet()
    {
        return new OkResult();
    }

    [HttpPatch]
    [Route("FeedPet")]
    public async Task<ActionResult> FeedPet(Guid Id)
    {
        return new OkResult();
    }
    
    [HttpPatch]
    [Route("GetDrinkPet")]
    public async Task<ActionResult> GetDrinkPet(Guid Id)
    {
        return new OkResult();
    }
}
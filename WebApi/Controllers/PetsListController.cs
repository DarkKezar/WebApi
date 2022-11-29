using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsListController : Controller
{
    //some private readonly service

    [HttpGet]
    [Route("GetPets")]
    public async Task<ActionResult<List<Pet>>> GetPetsAsync(Guid FarmId)
    {
        return new List<Pet>();
    }
    
    [HttpPatch]
    [Route("FeedPets")]
    public async Task<ActionResult> FeedPetsAsync(List<Guid> ids)
    {
        return new OkResult();
    }
    
    [HttpPatch]
    [Route("GetDrinkPets")]
    public async Task<ActionResult> GetDrinkPetsAsync(List<Guid> ids)
    {
        return new OkResult();
    }
}
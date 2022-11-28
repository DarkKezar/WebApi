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
    public async Task<List<Pet>> GetPets()
    {
        return new List<Pet>();
    }
    
    [HttpPatch]
    [Route("FeedPets")]
    public async Task<ActionResult> FeedPets(Guid Id)
    {
        return new OkResult();
    }
    
    [HttpPatch]
    [Route("GetDrinkPets")]
    public async Task<ActionResult> GetDrinkPets(Guid Id)
    {
        return new OkResult();
    }
}
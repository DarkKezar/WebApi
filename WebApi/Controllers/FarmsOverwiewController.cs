using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FarmsOverwiewController : Controller
{
    //some private readonly service

    [HttpGet]
    [Route("MyFarm")]
    public async Task<Farm> GetMyFarm()
    {
        return new Farm();
    }

    [HttpGet]
    [Route("CollabFarms")]
    public async Task<List<Farm>> GetCollabFarms()
    {
        return new List<Farm>();
    }

    
    [HttpPost]
    [Route("CreateFarm")]
    public async Task<ActionResult> CreateNewFarm()
    {
        return new OkResult();
    }
}
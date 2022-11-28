using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FarmsOverwiewController : Controller
{
    //some private readonly service

    [HttpGet]
    [Route("MyFarm")]
    public async Task<Farm> GetMyFarmAsync()
    {
        return new Farm();
    }

    [HttpGet]
    [Route("CollabFarms")]
    public async Task<List<Farm>> GetCollabFarmsAsync()
    {
        return new List<Farm>();
    }

    
    [HttpPost]
    [Route("CreateFarm")]
    public async Task<ActionResult> CreateNewFarmAsync(FarmCreationModel model)
    {
        return new OkResult();
    }
}
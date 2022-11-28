using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FarmDetailsController : Controller
{
    //some private readonly service

    [HttpGet]
    [Route("Farm")]
    public async Task<Farm> FarmInfo(Guid Id)
    {
        return new Farm();
    }
    
    [HttpPost]
    [Route("AddUser")]
    public async Task<ActionResult> InviteFriend(string email)
    {
        return new OkResult();
    }
}
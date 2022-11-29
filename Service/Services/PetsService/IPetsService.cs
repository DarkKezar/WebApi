using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service.Services.PetsService;

public interface IPetsService
{
    public Task<ActionResult<List<Pet>>> GetPetsAsync(Guid farmId);
    public Task<ActionResult> FeedPetsAsync(User user, List<Guid> ids);
    public Task<ActionResult> GetDrinkPetsAsync(User user, List<Guid> ids);
}
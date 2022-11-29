using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service.Services.PetsService;

public interface IPetsService
{
    public Task<ActionResult<List<Pet>>> GetPetsAsync(Guid userId, Guid farmId);
    public Task<ActionResult> FeedPetsAsync(Guid userId, List<Guid> ids);
    public Task<ActionResult> GetDrinkPetsAsync(Guid userId, List<Guid> ids);
}
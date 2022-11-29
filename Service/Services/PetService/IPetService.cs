using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace Service.Services.PetService;

public interface IPetService
{
    public Task<ActionResult<Pet>> GetPetAsync(Guid id);
    public Task<ActionResult> CreatePetAsync(Guid userId, PetCreationModel model);
    public Task<ActionResult> FeedPetAsync(Guid id);
    public Task<ActionResult> GetDrinkPetAsync(Guid id);
}
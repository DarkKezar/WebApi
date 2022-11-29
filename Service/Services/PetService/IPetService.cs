using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace Service.Services.PetService;

public interface IPetService
{
    public Task<ActionResult<Pet>> GetPetAsync(Guid id);
    public Task<ActionResult> CreatePetAsync(User user, PetCreationModel model);
    public Task<ActionResult> FeedPetAsync(User user, Guid id);
    public Task<ActionResult> GetDrinkPetAsync(User user, Guid id);
}
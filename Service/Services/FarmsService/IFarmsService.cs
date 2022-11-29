using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace Service.Services.FarmsService;

public interface IFarmsService
{
    public Task<ActionResult<Farm>> GetMyFarmAsync(User user);
    public Task<ActionResult<List<Farm>>> GetCollabFarmsAsync(User user);
    public Task<ActionResult> CreateNewFarmAsync(User user, FarmCreationModel model);
}
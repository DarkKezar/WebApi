using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace Service.Services.FarmsService;

public interface IFarmsService
{
    public Task<ActionResult<Farm>> GetMyFarmAsync(Guid userId);
    public Task<ActionResult<List<Farm>>> GetCollabFarmsAsync(Guid userId);
    public Task<ActionResult> CreateNewFarmAsync(Guid userId, FarmCreationModel model);
}
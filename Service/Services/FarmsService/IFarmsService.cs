using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace Service.Services.FarmsService;

public interface IFarmsService
{
    public Task<ActionResult<Farm>> GetMyFarmAsync(Guid id);
    public Task<ActionResult<List<Farm>>> GetCollabFarmsAsync(Guid id);
    public Task<ActionResult> CreateNewFarmAsync(Guid id, FarmCreationModel model);
}
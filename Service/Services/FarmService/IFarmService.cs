using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service.Services.FarmService;

public interface IFarmService
{
    public Task<ActionResult<Farm>> GetFarmInfoAsync(Guid Id);
    public Task<ActionResult> InviteFriendAsync(Guid currentUserId, string email);
}
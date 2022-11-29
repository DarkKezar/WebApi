using AutoMapper;
using Core.Entities;
using Core.Repositories.AccountRepository;
using Core.Repositories.FarmRepository;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace Service.Services.FarmsService;

public class FarmsService : IFarmsService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IFarmRepository _farmRepository;
    private readonly IMapper _mapper;

    public async Task<ActionResult<Farm>> GetMyFarmAsync(Guid userId)
    {
        return (await _accountRepository.ReadUserAsync(userId)).MyFarm;
    }

    public async Task<ActionResult<List<Farm>>> GetCollabFarmsAsync(Guid userId)
    {
        return (await _accountRepository.ReadUserAsync(userId)).Collaborations;
    }

    public async Task<ActionResult> CreateNewFarmAsync(Guid userId, FarmCreationModel model)
    {
        Farm newFarm = _mapper.Map<Farm>(model);
        newFarm.FarmOwner = await _accountRepository.ReadUserAsync(userId);
        try
        {
            await _farmRepository.CreateFarmAsync(newFarm);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new StatusCodeResult(500);
        }
    }
}
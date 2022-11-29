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

    public async Task<ActionResult<Farm>> GetMyFarmAsync(User user)
    {
        return user.MyFarm;
    }

    public async Task<ActionResult<List<Farm>>> GetCollabFarmsAsync(User user)
    {
        return user.Collaborations;
    }

    public async Task<ActionResult> CreateNewFarmAsync(User user, FarmCreationModel model)
    {
        Farm newFarm = _mapper.Map<Farm>(model);
        newFarm.FarmOwner = user;
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
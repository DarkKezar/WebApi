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

    public FarmsService(IAccountRepository accountRepository, IFarmRepository farmRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _farmRepository = farmRepository;
        _mapper = mapper;
    }

    public async Task<ActionResult<Farm>> GetMyFarmAsync(Guid id)
    {
        User user = await _accountRepository.ReadUserAsync(id);
        return await _farmRepository.ReadMyFarmAsync(user.MyFarm.Id);;
    }
    
    

    public async Task<ActionResult<List<Farm>>> GetCollabFarmsAsync(Guid id)
    {
        User user = await _accountRepository.ReadUserAsync(id);
        return await _farmRepository.ReadMyCollabFarmsAsync(user);
    }

    public async Task<ActionResult> CreateNewFarmAsync(Guid id, FarmCreationModel model)
    {
        User user = await _accountRepository.ReadUserAsync(id);
        if (user.MyFarm != null) return new BadRequestObjectResult("Farm already exist. User can't have more than 1 farm");
        Farm newFarm = _mapper.Map<Farm>(model);
        newFarm.userId = user.Id;
        user.MyFarm = newFarm;
        try
        {
            await _farmRepository.CreateFarmAsync(newFarm);
            await _accountRepository.UpdateUserAsync(user);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new StatusCodeResult(500);
        }
    }
}
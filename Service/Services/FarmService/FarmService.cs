using Core.Entities;
using Core.Repositories.AccountRepository;
using Core.Repositories.FarmRepository;
using Microsoft.AspNetCore.Mvc;

namespace Service.Services.FarmService;

public class FarmService : IFarmService
{
    private readonly IFarmRepository _farmRepository;
    private readonly IAccountRepository _accountRepository;

    public FarmService(IFarmRepository farmRepository, IAccountRepository accountRepository)
    {
        _farmRepository = farmRepository;
        _accountRepository = accountRepository;
    }

    public async Task<ActionResult<Farm>> GetFarmInfoAsync(Guid Id)
    {
        try
        {
            return await _farmRepository.ReadMyFarmAsync(Id);
        }
        catch (Exception e)
        {
            return new NotFoundResult();
        }
    }

    public async Task<ActionResult> InviteFriendAsync(Guid id, string email)
    {
        User userToAdd;
        User currentUser;
        try
        {
            userToAdd = await _accountRepository.ReadUserAsync(email);
            currentUser = await _accountRepository.ReadUserAsync(id);
        }catch (Exception e)
        {
            return new NotFoundResult();
        }

        Farm farm = currentUser.MyFarm;
        if (farm == null) return new NotFoundObjectResult("You have not your own farm");
        else
        {
            userToAdd.CollaborationsId.Add(farm.Id);
            await _accountRepository.UpdateUserAsync(userToAdd);
            await _farmRepository.UpdateFarmAsync(farm);
        
            return new OkResult();
        }
    }
}
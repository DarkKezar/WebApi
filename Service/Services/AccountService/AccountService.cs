using System.Security.Claims;
using AutoMapper;
using Core.Entities;
using Core.Repositories.AccountRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace Service.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<ActionResult<UserShowModel>> GetUserDataAsync(Guid id)
    {
        return _mapper.Map<UserShowModel>(await _accountRepository.ReadUserAsync(id));
    }

    public async Task<ActionResult> SignUpAsync(UserCreationModel model)
    {
        try
        {
            await _accountRepository.CreateUserAsync(_mapper.Map<User>(model), model.Password);
            return new OkResult(); 
            //Redirect in frontend
        }
        catch (Exception e)
        {
            return new StatusCodeResult(500);
        }
    }

    public async Task<IdentityResult> ChangePasswordAsync(UserUpdateModel model, Guid id)
    { 
        User user = await _accountRepository.ReadUserAsync(id);
       return await _accountRepository.UpdatePasswordAsync(user, model.OldPassword, model.NewPassword);
    }

    public async Task<ActionResult> ChangeEmailAsync(UserUpdateModel model, Guid id)
    {
        User user = await _accountRepository.ReadUserAsync(id);
        user = _mapper.Map<User>(user);
        await _accountRepository.UpdateUserAsync(user);
        return new OkResult();
    }

    public async Task<ActionResult> ChangePhotoAsync(UserUpdateModel model, Guid id)
    {
        return await this.ChangeEmailAsync(model, id);
    }

    public async Task<ActionResult> ChangeUserNameAsync(UserUpdateModel model, Guid id)
    {
        return await this.ChangeEmailAsync(model, id);
    }
}
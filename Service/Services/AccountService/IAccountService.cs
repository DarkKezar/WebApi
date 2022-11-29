using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace Service.Services.AccountService;

public interface IAccountService
{
    public Task<ActionResult<UserShowModel>> GetUserDataAsync(User user);
    public Task<ActionResult> SignUpAsync(UserCreationModel model);
    public Task<IdentityResult> ChangePasswordAsync(UserUpdateModel model, User user);
    public Task<ActionResult> ChangeEmailAsync(UserUpdateModel model, User user);
    public Task<ActionResult> ChangePhotoAsync(UserUpdateModel model, User user);
    public Task<ActionResult> ChangeUserNameAsync(UserUpdateModel model, User user);
}
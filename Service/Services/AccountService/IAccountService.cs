using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace Service.Services.AccountService;

public interface IAccountService
{
    public Task<ActionResult<UserShowModel>> GetUserDataAsync(Guid id);
    public Task<ActionResult> SignUpAsync(UserCreationModel model);
    public Task<ActionResult> ChangePasswordAsync(UserUpdateModel model, Guid id);
    public Task<ActionResult> ChangeEmailAsync(UserUpdateModel model, Guid id);
    public Task<ActionResult> ChangePhotoAsync(UserUpdateModel model, Guid id);
    public Task<ActionResult> ChangeUserNameAsync(UserUpdateModel model, Guid id);
}
using Core.Entities;

namespace Core.Repositories.UserActionRepository;

public interface IUserActionRepository
{
    public Task CreateUserActionAsync(UserAction action);
    public Task DeleteUserActionAsync(UserAction action);
    public Task<UserAction> ReadUserActionAsync(Guid id);
    public Task<UserAction> ReadLastUserActionAsync(Guid petId, ActionEnum type);
    public Task<List<UserAction>> ReadAllUserActionAsync(User user);
    public Task<List<UserAction>> ReadAllUserActionAsync(Pet   pet);
}
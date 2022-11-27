using Core.Entities;

namespace Core.Repositories.AccountRepository;

public interface IAccountRepository
{
    public Task CreateUserAsync(User user, string password);
    public Task DeleteUserAsync(User user);
    public Task UpdateUserAsync(User user);
    public Task<User> ReadUserAsync(Guid id);
    public Task<List<User>> ReadAllUserAsync(int Count, int Page);
}
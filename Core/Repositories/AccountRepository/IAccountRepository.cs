using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Repositories.AccountRepository;

public interface IAccountRepository
{
    public Task CreateUserAsync(User user, string password);
    public Task DeleteUserAsync(User user);
    public Task UpdateUserAsync(User user);
    public Task<IdentityResult> UpdatePasswordAsync(User user, string oldPassword, string newPassword);
    public Task<User> ReadUserAsync(Guid id);
    public Task<User> ReadUserAsync(string email);
    public Task<List<User>> ReadAllUserAsync(int Count, int Page);
}
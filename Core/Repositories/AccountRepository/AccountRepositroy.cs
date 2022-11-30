using Core.Context;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.AccountRepository;

public class AccountRepositroy : IAccountRepository
{
    private readonly PetContext _context;
    private readonly UserManager<User> _userManager;

    public AccountRepositroy(PetContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }


    public async Task CreateUserAsync(User user, string password)
    {
        user.UserName = user.Email;
        user.NormalizedUserName = user.Email.ToUpper();
        user.CollaborationsId = new List<Guid>();
        IdentityResult result = await _userManager.CreateAsync(user, password);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        user.isDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IdentityResult> UpdatePasswordAsync(User user, string oldPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(user, oldPassword,  newPassword);
    }

    public async Task<User> ReadUserAsync(Guid id)
    {
        return await _context.Users.Include(u => u.MyFarm).FirstAsync(u => u.Id == id);
    }
    public async Task<User> ReadUserAsync(string email)
    {
        return await _context.Users.FirstAsync(u => u.NormalizedEmail == email.ToUpper());
    }

    public async Task<List<User>> ReadAllUserAsync(int Count, int Page)
    {
        return await _context.Users.Skip(Count * Page).Take(Page).ToListAsync();
    }
}
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppContext = Core.Context.AppContext;

namespace Core.Repositories.AccountRepository;

public class AccountRepositroy : IAccountRepository
{
    private readonly AppContext _context;
    private readonly UserManager<User> _userManager;

    public AccountRepositroy(AppContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }


    public async Task CreateUserAsync(User user, string password)
    {
        await _userManager.CreateAsync(user, password);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        user.isDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userManager.UpdateAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> ReadUserAsync(Guid id)
    {
        return await _context.Users.FirstAsync(u => u.Id == id);
    }

    public async Task<List<User>> ReadAllUserAsync(int Count, int Page)
    {
        return await _context.Users.Skip(Count * Page).Take(Page).ToListAsync();
    }
}
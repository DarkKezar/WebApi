using Core.Context;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.UserActionRepository;

public class UserActionRepository : IUserActionRepository
{
    private readonly PetContext _context;

    public UserActionRepository(PetContext context)
    {
        _context = context;
    }

    public async Task CreateUserActionAsync(UserAction action)
    {
        await _context.Actions.AddAsync(action);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserActionAsync(UserAction action)
    {
        _context.Actions.Remove(action);
        await _context.SaveChangesAsync();
    }

    public async Task<UserAction> ReadUserActionAsync(Guid id)
    {
        return await _context.Actions.FirstAsync(a => a.Id == id);
    }

    public async Task<UserAction> ReadLastUserActionAsync(Guid petId, ActionEnum type)
    {
        return await _context.Actions.OrderBy(a => a.Date).
                            FirstOrDefaultAsync(p => p.Pet.Id == petId && p.Action == type);
    }

    public async Task<List<UserAction>> ReadAllUserActionAsync(User user)
    {
        return await _context.Actions.Where(a => a.User == user).ToListAsync();
    }

    public async Task<List<UserAction>> ReadAllUserActionAsync(Pet pet)
    {
        return await _context.Actions.Where(a => a.Pet == pet).ToListAsync();
    }
}
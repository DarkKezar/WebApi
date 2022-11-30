using Core.Entities;
using Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.FarmRepository;

public class FarmRepository : IFarmRepository
{
    private readonly PetContext _context;

    public FarmRepository(PetContext context)
    {
        _context = context;
    }

    public async Task CreateFarmAsync(Farm farm)
    {
        await _context.Farms.AddAsync(farm);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFarmAsync(Farm farm)
    {
        _context.Farms.Remove(farm);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFarmAsync(Farm farm)
    {
        _context.Farms.Update(farm);
        await _context.SaveChangesAsync();
    }

    public async Task<Farm> ReadMyFarmAsync(Guid Id)
    {
        return await _context.Farms.Include(f => f.Pets).ThenInclude(p => p.Stats).FirstAsync(f => f.Id == Id);
    }

    public async Task<List<Farm>> ReadMyCollabFarmsAsync(User user)
    {
        return await _context.Farms.Where(f => user.CollaborationsId.Contains(f.Id)).Include(f => f.Pets).ThenInclude(p => p.Stats).ToListAsync();
    }
}
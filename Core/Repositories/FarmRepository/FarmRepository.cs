using Core.Entities;
using Core.Context;
using Microsoft.EntityFrameworkCore;
using AppContext = Core.Context.AppContext;

namespace Core.Repositories.FarmRepository;

public class FarmRepository : IFarmRepository
{
    private readonly AppContext _context;

    public FarmRepository(AppContext context)
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

    public async Task<Farm> ReadMyFarmAsync(Guid userId)
    {
        return await _context.Farms.FirstAsync(f => f.FarmOwner.Id == userId);
    }

    public async Task<List<Farm>> ReadMyCollabFarmsAsync(Guid userId)
    {
        return await _context.Farms.Where(f => f.FarmCollabersId.Contains(userId)).ToListAsync();
    }
}
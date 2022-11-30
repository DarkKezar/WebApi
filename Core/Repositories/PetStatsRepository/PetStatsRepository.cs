using Core.Entities;
using Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.PetStatsRepository;

public class PetStatsRepository : IPetStatsRepository
{
    private readonly PetContext _context;

    public PetStatsRepository(PetContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CreatePetStatsAsync(PetStats stats)
    {
        await _context.PetStats.AddAsync(stats);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePetStatsAsync(PetStats stats)
    {
        _context.PetStats.Remove(stats);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePetStatsAsync(PetStats stats)
    {
        _context.PetStats.Update(stats);
        await _context.SaveChangesAsync();
    }

    public async Task<PetStats> ReadPetStatsAsync(Guid Id)
    {
        return (await _context.Pets.FirstAsync(p => p.Id == Id)).Stats;
    }
/*
    public async Task<List<PetStats>> ReadPetStatsAsync(List<Guid> petsId)
    {
        return await _context.PetStats.Where(s => petsId.Contains(s.Pet.Id)).ToListAsync();
    }*/

    public async Task<List<PetStats>> ReadAllPetStatsAsync(int Count, int Page)
    {
        return await _context.PetStats.Skip(Count * Page).Take(Count).ToListAsync();
    }
}
using Core.Entities;
using Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.PetRepository;

public class PetRepository : IPetRepository
{
    private readonly PetContext _context;

    public PetRepository(PetContext context)
    {
        _context = context;
    }

    public async Task CreatePetAsync(Pet pet, PetStats stats)
    {
        await _context.Pets.AddAsync(pet);
        await _context.PetStats.AddAsync(stats);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePetAsync(Pet pet)
    {
        _context.Remove(pet);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePetAsync(Pet pet)
    {
        _context.Update(pet);
        await _context.SaveChangesAsync();
    }

    public async Task<Pet> ReadPetAsync(Guid Id)
    {
        return await _context.Pets.Include(p => p.Stats).FirstAsync(p => p.Id == Id);
    }

    public async Task<List<Pet>> ReadAllPetsAsync(Guid farmId)
    {
        return (await _context.Farms.Include(f => f.Pets).ThenInclude(p => p.Stats).FirstAsync(f => f.Id == farmId)).Pets;
    }

    public async Task<bool> isExistAsync(string name)
    {
        return (await _context.Pets.CountAsync(p => p.Name == name)) > 0;
    }
}
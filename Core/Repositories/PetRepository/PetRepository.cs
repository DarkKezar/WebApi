using Core.Entities;
using Core.Context;
using Microsoft.EntityFrameworkCore;
using AppContext = Core.Context.AppContext;

namespace Core.Repositories.PetRepository;

public class PetRepository : IPetRepository
{
    private readonly AppContext _context;

    public PetRepository(AppContext context)
    {
        _context = context;
    }

    public async Task CreatePetAsync(Pet pet)
    {
        await _context.Pets.AddAsync(pet);
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
        return await _context.Pets.FirstAsync(p => p.Id == Id);
    }

    public async Task<List<Pet>> ReadAllPetsAsync(Guid farmId)
    {
        return await _context.Pets.Where(p => p.Farm.Id == farmId).ToListAsync();
    }
}
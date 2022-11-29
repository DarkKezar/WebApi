using Core.Entities;

namespace Core.Repositories.PetRepository;

public interface IPetRepository
{
    public Task CreatePetAsync(Pet pet, PetStats stats);
    public Task DeletePetAsync(Pet pet);
    public Task UpdatePetAsync(Pet pet);
    public Task<Pet> ReadPetAsync(Guid Id);
    public Task<List<Pet>> ReadAllPetsAsync(Guid farmId);
    public Task<bool> isExistAsync(string name);
}
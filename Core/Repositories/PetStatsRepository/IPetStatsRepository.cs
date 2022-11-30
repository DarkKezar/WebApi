using Core.Entities;

namespace Core.Repositories.PetStatsRepository;

public interface IPetStatsRepository
{
    public Task CreatePetStatsAsync(PetStats stats);
    public Task DeletePetStatsAsync(PetStats stats);
    public Task UpdatePetStatsAsync(PetStats stats);
    public Task<PetStats> ReadPetStatsAsync(Guid Id);
   /* public Task<List<PetStats>> ReadPetStatsAsync(List<Guid> petsId);*/
    public Task<List<PetStats>> ReadAllPetStatsAsync(int Count, int Page);
}
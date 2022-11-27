using Core.Entities;

namespace Core.Repositories.FarmRepository;

public interface IFarmRepository
{
    public Task CreateFarmAsync(Farm farm);
    public Task DeleteFarmAsync(Farm farm);
    public Task UpdateFarmAsync(Farm farm);
    public Task<Farm> ReadMyFarmAsync(Guid userId);
    public Task<List<Farm>> ReadMyCollabFarmsAsync(Guid userId);
}
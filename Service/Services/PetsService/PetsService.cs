using Core.Entities;
using Core.Repositories.AccountRepository;
using Core.Repositories.PetRepository;
using Core.Repositories.PetStatsRepository;
using Core.Repositories.UserActionRepository;
using Microsoft.AspNetCore.Mvc;

namespace Service.Services.PetsService;

public class PetsService : IPetsService
{
    private readonly IPetRepository _petRepository;
    private readonly IPetStatsRepository _statsRepository;

    public PetsService(IPetRepository petRepository, IPetStatsRepository statsRepository, IUserActionRepository actionRepository)
    {
        _petRepository = petRepository;
        _statsRepository = statsRepository;
        _actionRepository = actionRepository;
    }

    private readonly IUserActionRepository _actionRepository;
    private const int feedTime = 1;

    public async Task<ActionResult<List<Pet>>> GetPetsAsync(Guid farmId)
    {
        try
        {
            return await _petRepository.ReadAllPetsAsync(farmId);
        }
        catch (Exception e)
        {
            return new NotFoundResult();
        }
    }

    public async Task<ActionResult> FeedPetsAsync(Guid userId, List<Guid> ids)
    {
        List<ActionResult> answer = new List<ActionResult>();
        foreach (Guid id in ids)
        {
            if ((await _actionRepository.ReadLastUserActionAsync(id, ActionEnum.FEED)).Date.AddDays(feedTime) < DateTime.Now)
            {
                PetStats stats = await _statsRepository.ReadPetStatsAsync(id);
                switch (stats.HungerLevel)
                {
                    case HungerLevelEnum.DEAD:
                        answer.Add( new BadRequestObjectResult("Pet is dead") );
                        break;
                    case HungerLevelEnum.FULL:
                        answer.Add( new BadRequestObjectResult("Pet is full") );
                        break;
                    default:
                        stats.HungerLevel++;
                        await _statsRepository.UpdatePetStatsAsync(stats);
                        answer.Add( new OkResult());
                        break;
                }
            }
            else answer.Add(new BadRequestObjectResult("Already feed"));
        }

        return new OkObjectResult(answer);
    }

    public async Task<ActionResult> GetDrinkPetsAsync(Guid userId, List<Guid> ids)
    {
        List<ActionResult> answer = new List<ActionResult>();
        foreach (Guid id in ids)
        {
            if ((await _actionRepository.ReadLastUserActionAsync(id, ActionEnum.DRINK)).Date.AddDays(feedTime) < DateTime.Now)
            {
                PetStats stats = await _statsRepository.ReadPetStatsAsync(id);
                switch (stats.ThirstyLevel)
                {
                    case ThirstyLevelEnum.DEAD:
                        answer.Add( new BadRequestObjectResult("Pet is dead") );
                        break;
                    case ThirstyLevelEnum.FULL:
                        answer.Add( new BadRequestObjectResult("Pet is full") );
                        break;
                    default:
                        stats.ThirstyLevel++;
                        await _statsRepository.UpdatePetStatsAsync(stats);
                        answer.Add( new OkResult());
                        break;
                }
            }
            else answer.Add(new BadRequestObjectResult("Already feed"));
        }

        return new OkObjectResult(answer);
    }
}
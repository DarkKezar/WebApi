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
    private readonly IAccountRepository _accountRepository;

    public PetsService(IPetRepository petRepository, IPetStatsRepository statsRepository, IAccountRepository accountRepository, IUserActionRepository actionRepository)
    {
        _petRepository = petRepository;
        _statsRepository = statsRepository;
        _accountRepository = accountRepository;
        _actionRepository = actionRepository;
    }

    private readonly IUserActionRepository _actionRepository;
    private const int feedTime = 1;

    public async Task<ActionResult<List<Pet>>> GetPetsAsync(Guid farmId)
    {
        try
        {
            List<Pet> pets = await _petRepository.ReadAllPetsAsync(farmId);
            foreach (Pet pet in pets)
            {
                if (pet.DeathDate == null)
                {
                    UserAction userActionFEED = (await _actionRepository.ReadLastUserActionAsync(pet.Id, ActionEnum.FEED));
                    UserAction userActionDRINK = (await _actionRepository.ReadLastUserActionAsync(pet.Id, ActionEnum.DRINK));
                    if ((userActionFEED != null && userActionFEED.Date.AddDays(feedTime) < DateTime.UtcNow) ||
                        (userActionFEED == null && pet.BirthDate.AddDays(feedTime) < DateTime.UtcNow))
                    {
                        pet.Stats.HungerLevel--;
                        if(pet.Stats.HungerLevel == HungerLevelEnum.DEAD)
                            pet.DeathDate = DateTime.UtcNow;
                    }else 

                    if ((userActionDRINK != null && userActionDRINK.Date.AddDays(feedTime) < DateTime.UtcNow) ||
                        (userActionDRINK == null && pet.BirthDate.AddDays(feedTime) < DateTime.UtcNow))
                    {
                        pet.Stats.ThirstyLevel--;
                        if(pet.Stats.ThirstyLevel == ThirstyLevelEnum.DEAD)
                            pet.DeathDate = DateTime.UtcNow;
                    }
                    await _petRepository.UpdatePetAsync(pet);
                    await _statsRepository.UpdatePetStatsAsync(pet.Stats);
                }
            }
            return pets;
        }
        catch (Exception e)
        {
            return new NotFoundResult();
        }
    }

    public async Task<ActionResult> FeedPetsAsync(Guid userId, List<Guid> ids)
    {
        User user = await _accountRepository.ReadUserAsync(userId);
        List<ActionResult> answer = new List<ActionResult>();
        foreach (Guid id in ids)
        {
            UserAction userAction = (await _actionRepository.ReadLastUserActionAsync(id, ActionEnum.FEED));
            if (userAction == null || userAction.Date.AddDays(feedTime) < DateTime.Now)
            {
                Pet pet = await _petRepository.ReadPetAsync(id);
                switch (pet.Stats.HungerLevel)
                {
                    case HungerLevelEnum.DEAD:
                        answer.Add( new BadRequestObjectResult("Pet is dead") );
                        break;
                    case HungerLevelEnum.FULL:
                        answer.Add( new BadRequestObjectResult("Pet is full") );
                        break;
                    default:
                        pet.Stats.HungerLevel++;
                        UserAction action = new UserAction();
                        action.Action = ActionEnum.FEED;
                        action.Date = DateTime.UtcNow;
                        action.Pet = await _petRepository.ReadPetAsync(id);
                        action.User = user;
                        await _statsRepository.UpdatePetStatsAsync(pet.Stats);
                        await _actionRepository.CreateUserActionAsync(action);
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
        User user = await _accountRepository.ReadUserAsync(userId);
        List<ActionResult> answer = new List<ActionResult>();
        foreach (Guid id in ids)
        {
            UserAction userAction = (await _actionRepository.ReadLastUserActionAsync(id, ActionEnum.DRINK));
            if (userAction == null || userAction.Date.AddDays(feedTime) < DateTime.Now)
            {
                Pet pet = await _petRepository.ReadPetAsync(id);
                switch (pet.Stats.ThirstyLevel)
                {
                    case ThirstyLevelEnum.DEAD:
                        answer.Add( new BadRequestObjectResult("Pet is dead") );
                        break;
                    case ThirstyLevelEnum.FULL:
                        answer.Add( new BadRequestObjectResult("Pet is full") );
                        break;
                    default:
                        pet.Stats.ThirstyLevel++;
                        UserAction action = new UserAction();
                        action.Action = ActionEnum.DRINK;
                        action.Date = DateTime.UtcNow;
                        action.Pet = await _petRepository.ReadPetAsync(id);
                        action.User = user;
                        await _actionRepository.CreateUserActionAsync(action);
                        await _statsRepository.UpdatePetStatsAsync(pet.Stats);
                        answer.Add( new OkResult());
                        break;
                }
            }
            else answer.Add(new BadRequestObjectResult("Already feed"));
        }

        return new OkObjectResult(answer);
    }
}
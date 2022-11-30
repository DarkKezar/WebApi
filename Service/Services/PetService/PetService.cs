using AutoMapper;
using Core.Entities;
using Core.Repositories.AccountRepository;
using Core.Repositories.FarmRepository;
using Core.Repositories.PetRepository;
using Core.Repositories.PetStatsRepository;
using Core.Repositories.UserActionRepository;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;

namespace Service.Services.PetService;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IPetStatsRepository _statsRepository;
    private readonly IUserActionRepository _actionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IFarmRepository _farmRepository;
    private readonly IMapper _mapper;
    private const int feedTime = 1;


    public PetService(IPetRepository petRepository, IPetStatsRepository statsRepository, IUserActionRepository actionRepository, IAccountRepository accountRepository, IFarmRepository farmRepository, IMapper mapper)
    {
        _petRepository = petRepository;
        _statsRepository = statsRepository;
        _actionRepository = actionRepository;
        _accountRepository = accountRepository;
        _farmRepository = farmRepository;
        _mapper = mapper;
    }

    public async Task<ActionResult<Pet>> GetPetAsync(Guid id)
    {
        try
        {
            return await _petRepository.ReadPetAsync(id);
        }
        catch (Exception e)
        {
            return new NotFoundResult();
        }
        
    }

    public async Task<ActionResult> CreatePetAsync(Guid userId, PetCreationModel model)
    {
        User user = await _accountRepository.ReadUserAsync(userId);
        if (!(await _petRepository.isExistAsync(model.Name)))
        {
            Pet newPet = _mapper.Map<Pet>(model);
            PetStats newPetStats = new PetStats();


            newPetStats.HungerLevel = HungerLevelEnum.FULL;
            newPetStats.ThirstyLevel = ThirstyLevelEnum.FULL;
            newPetStats.HappyDaysCount = 0;
            newPet.BirthDate = DateTime.UtcNow;
            newPet.DeathDate = null;
            newPet.Stats = newPetStats;
            if (user.MyFarm.Pets == null)
                user.MyFarm.Pets = new List<Pet>();
            user.MyFarm.Pets.Add(newPet);

            
            await _petRepository.CreatePetAsync(newPet, newPetStats);
            await _farmRepository.UpdateFarmAsync(user.MyFarm);
            return new OkResult();
        }
        else return new BadRequestObjectResult("Already exist");
    }

    public async Task<ActionResult> FeedPetAsync(Guid userId, Guid id)
    {
        User user = await _accountRepository.ReadUserAsync(userId);
        UserAction userAction = (await _actionRepository.ReadLastUserActionAsync(id, ActionEnum.FEED));
        if (userAction == null || userAction.Date.AddDays(feedTime) < DateTime.Now)
        {
            Pet pet = await _petRepository.ReadPetAsync(id);
            switch (pet.Stats.HungerLevel)
            {
                case HungerLevelEnum.DEAD:
                    return new BadRequestObjectResult("Pet is dead");
                case HungerLevelEnum.FULL:
                    return new BadRequestObjectResult("Pet is full");
                default:
                    pet.Stats.HungerLevel++;
                    UserAction action = new UserAction();
                    action.Action = ActionEnum.FEED;
                    action.Date = DateTime.UtcNow;
                    action.Pet = await _petRepository.ReadPetAsync(id);
                    action.User = user;
                    await _actionRepository.CreateUserActionAsync(action);
                    await _statsRepository.UpdatePetStatsAsync(pet.Stats);
                    return new OkResult();
            }
        }
        else return new BadRequestObjectResult("Already feed");
    }

    public async Task<ActionResult> GetDrinkPetAsync(Guid userId, Guid id)
    {
        UserAction userAction = (await _actionRepository.ReadLastUserActionAsync(id, ActionEnum.DRINK));
        if (userAction == null || userAction.Date.AddDays(feedTime) < DateTime.Now)
        {
            User user = await _accountRepository.ReadUserAsync(userId);
            Pet pet = await _petRepository.ReadPetAsync(id);
            switch (pet.Stats.ThirstyLevel)
            {
                case ThirstyLevelEnum.DEAD:
                    return new BadRequestObjectResult("Pet is dead");
                case ThirstyLevelEnum.FULL:
                    return new BadRequestObjectResult("Pet is full");
                default:
                    pet.Stats.ThirstyLevel++;
                    UserAction action = new UserAction();
                    action.Action = ActionEnum.DRINK;
                    action.Date = DateTime.UtcNow;
                    action.Pet = await _petRepository.ReadPetAsync(id);
                    action.User = user;
                    await _actionRepository.CreateUserActionAsync(action);
                    await _statsRepository.UpdatePetStatsAsync(pet.Stats);
                    return new OkResult();
            }
        }
        else return new BadRequestObjectResult("Already drink");
    }
}
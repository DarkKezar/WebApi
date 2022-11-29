using AutoMapper;
using Core.Entities;
using Core.Repositories.AccountRepository;
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
    private readonly IMapper _mapper;
    private const int feedTime = 1;
    

    public PetService(IPetRepository petRepository, IPetStatsRepository statsRepository, IUserActionRepository actionRepository, IAccountRepository accountRepository, IMapper mapper)
    {
        _petRepository = petRepository;
        _statsRepository = statsRepository;
        _actionRepository = actionRepository;
        _accountRepository = accountRepository;
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

    public async Task<ActionResult> CreatePetAsync(User user, PetCreationModel model)
    {
        if (!(await _petRepository.isExistAsync(model.Name)))
        {
            Pet newPet = _mapper.Map<Pet>(model);
            PetStats newPetStats = new PetStats();

            
            //In ideal condition this must be transaction
            await _statsRepository.CreatePetStatsAsync(newPetStats);
            newPet.Farm = user.MyFarm;
            newPet.Stats = newPetStats;
            await _petRepository.CreatePetAsync(newPet, newPetStats);
            //transaction end
            
            return new OkResult();
        }
        else return new BadRequestObjectResult("Already exist");
    }

    public async Task<ActionResult> FeedPetAsync(User user, Guid id)
    {
        if ((await _actionRepository.ReadLastUserActionAsync(id, ActionEnum.FEED)).Date.AddDays(feedTime) < DateTime.Now)
        {
            PetStats stats = await _statsRepository.ReadPetStatsAsync(id);
            switch (stats.HungerLevel)
            {
                case HungerLevelEnum.DEAD:
                    return new BadRequestObjectResult("Pet is dead");
                case HungerLevelEnum.FULL:
                    return new BadRequestObjectResult("Pet is full");
                default:
                    stats.HungerLevel++;
                    UserAction action = new UserAction();
                    action.Action = ActionEnum.FEED;
                    action.Date = DateTime.Now;
                    action.Pet = await _petRepository.ReadPetAsync(id);
                    action.User = user;
                    await _statsRepository.UpdatePetStatsAsync(stats);
                    await _actionRepository.CreateUserActionAsync(action);
                    await _statsRepository.UpdatePetStatsAsync(stats);
                    return new OkResult();
            }
        }
        else return new BadRequestObjectResult("Already feed");
    }

    public async Task<ActionResult> GetDrinkPetAsync(User user, Guid id)
    {
        if ((await _actionRepository.ReadLastUserActionAsync(id, ActionEnum.DRINK)).Date.AddDays(feedTime) < DateTime.Now)
        {
            PetStats stats = await _statsRepository.ReadPetStatsAsync(id);
            switch (stats.ThirstyLevel)
            {
                case ThirstyLevelEnum.DEAD:
                    return new BadRequestObjectResult("Pet is dead");
                case ThirstyLevelEnum.FULL:
                    return new BadRequestObjectResult("Pet is full");
                default:
                    stats.ThirstyLevel++;
                    UserAction action = new UserAction();
                    action.Action = ActionEnum.DRINK;
                    action.Date = DateTime.Now;
                    action.Pet = await _petRepository.ReadPetAsync(id);
                    action.User = user;
                    await _statsRepository.UpdatePetStatsAsync(stats);
                    await _actionRepository.CreateUserActionAsync(action);
                    await _statsRepository.UpdatePetStatsAsync(stats);
                    return new OkResult();
            }
        }
        else return new BadRequestObjectResult("Already drink");
    }
}
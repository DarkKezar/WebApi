using AutoMapper;
using Core.Entities;
using Service.DTO;

namespace Service.AutoMappers;

public class AppMappingFarm : Profile
{
    public AppMappingFarm()
    {			
        CreateMap<FarmCreationModel, Farm>();
    }
}
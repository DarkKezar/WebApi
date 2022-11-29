using AutoMapper;
using Core.Entities;
using Service.DTO;

namespace Service.AutoMappers;

public class AppMappingPet : Profile
{
    protected AppMappingPet()
    {
        CreateMap<PetCreationModel, Pet>();
    }
}
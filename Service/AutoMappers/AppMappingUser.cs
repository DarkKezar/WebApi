using AutoMapper;
using Core.Entities;
using Service.DTO;

namespace Service.AutoMappers;

public class AppMappingUser : Profile
{
    protected AppMappingUser()
    {
        CreateMap<UserCreationModel, User>();
        CreateMap<UserUpdateModel, User>();
        CreateMap<User, UserShowModel>();
    }
}
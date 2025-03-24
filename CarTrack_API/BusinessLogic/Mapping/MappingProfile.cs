using AutoMapper;
using CarTrack_API.DataAccess.Dtos;
using CarTrack_API.DataAccess.Dtos.LoginDtos;
using CarTrack_API.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserLoginResponseDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Role));
        
        CreateMap<UserLoginRequestDto, User>();
    }
}
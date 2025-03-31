using AutoMapper;
using CarTrack_API.DataAccess.Dtos;
using CarTrack_API.DataAccess.Dtos.LoginDtos;
using CarTrack_API.DataAccess.Dtos.RegisterDtos;
using CarTrack_API.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserLoginResponseDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.ClientProfile, opt => opt.MapFrom(src => src.ClientProfile))
            .ForMember(dest => dest.ManagerProfile, opt => opt.MapFrom(src => src.ManagerProfile))
            .ForMember(dest => dest.MechanicProfile, opt => opt.MapFrom(src => src.MechanicProfile));
        CreateMap<UserRegisterRequestDto, User>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            
    }
}
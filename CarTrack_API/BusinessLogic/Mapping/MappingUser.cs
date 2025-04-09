using CarTrack_API.EntityLayer.Dtos.UserDto.RegisterDtos;
using CarTrack_API.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingUser
{
    public static User ToUser(this UserRegisterRequestDto requestUser)
    {
        var user = new User
        {
            Username = requestUser.Username,
            Email = requestUser.Email,
            Password = requestUser.Password,
            PhoneNumber = requestUser.PhoneNumber,
            RoleId = requestUser.RoleId
        };

        return user;
    }
    
}
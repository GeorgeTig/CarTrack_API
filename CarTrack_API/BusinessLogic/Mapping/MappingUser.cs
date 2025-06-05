using CarTrack_API.EntityLayer.Dtos.UserDto;
using CarTrack_API.EntityLayer.Dtos.UserDto.RegisterDtos;
using CarTrack_API.EntityLayer.Models;

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

    public static UserResponseDto ToUserResponseDto(this User user)
    {
        var userResponse = new UserResponseDto
        {
            Username = user.Username,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
        };

        return userResponse;
    }
}
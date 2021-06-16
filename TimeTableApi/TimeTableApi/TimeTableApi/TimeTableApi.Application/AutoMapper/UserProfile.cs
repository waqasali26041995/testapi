using AutoMapper;
using TimeTableApi.Application.Shared.UserManagement.Dtos;
using TimeTableApi.Core.Entities;

namespace TimeTableApi.Application.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateOrEditUserInputDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}

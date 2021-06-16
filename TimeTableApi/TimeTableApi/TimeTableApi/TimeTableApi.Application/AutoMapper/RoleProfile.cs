using AutoMapper;
using TimeTableApi.Application.Shared.RoleManagement.Dtos;
using TimeTableApi.Core.Entities;

namespace TimeTableApi.Application.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, CreateOrEditRoleInputDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}

using System;
using TimeTableApi.Application.Shared.GenericDtos;

namespace TimeTableApi.Application.Shared.RoleManagement.Dtos
{
    public class CreateOrEditRoleInputDto : GenericDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

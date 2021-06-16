using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement.Dtos;
using TimeTableApi.Application.Shared.RoleManagement.Dtos;
using TimeTableApi.Application.Shared.UserManagement.Dtos;

namespace TimeTableApi.Application.Shared.RoleManagement
{
    public interface IRoleAppService
    {
        void CreateOrUpdate(CreateOrEditRoleInputDto input);
        void Remove(string id);
        List<RoleDto> GetAll();
        RoleDto GetById(string id);
        RoleDto GetByName(string name);
    }
}

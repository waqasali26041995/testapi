using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement.Dtos;
using TimeTableApi.Application.Shared.UserManagement.Dtos;

namespace TimeTableApi.Application.Shared.UserManagement
{
    public interface IUserAppService
    {
        void CreateOrUpdate(CreateOrEditUserInputDto input);
        void Remove(string id);
        List<UserDto> GetAll();
        UserDto GetById(string id);
        UserDto GetByEmail(LoginModel input);
    }
}

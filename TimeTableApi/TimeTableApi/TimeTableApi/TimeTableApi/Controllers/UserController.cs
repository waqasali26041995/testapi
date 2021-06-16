using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.UserManagement;
using TimeTableApi.Application.Shared.UserManagement.Dtos;

namespace TimeTableApi.Controllers
{
    [Route("User")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService) {
            _userAppService = userAppService;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<UserDto> GetAll()
        {
            return _userAppService.GetAll();
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public UserDto GetById(string id)
        {
            return _userAppService.GetById(id);
        }

        [HttpPost]
        [Route("CreateOrUpdate")]
        public void CreateOrUpdate([FromBody] CreateOrEditUserInputDto input)
        {
            _userAppService.CreateOrUpdate(input);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public void Delete(string id)
        {
            _userAppService.Remove(id);
        }
    }
}

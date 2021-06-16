using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement;
using TimeTableApi.Application.Shared.EventManagement.Dtos;
using TimeTableApi.Application.Shared.RoleManagement;
using TimeTableApi.Application.Shared.RoleManagement.Dtos;

namespace TimeTableApi.Controllers
{
    [Route("Role")]
    public class RoleController : Controller
    {
        private readonly IRoleAppService _roleAppService;

        public RoleController(IRoleAppService roleAppService) {
            _roleAppService = roleAppService;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<RoleDto> GetAll()
        {
            return _roleAppService.GetAll();
        }

        [HttpPost]
        [Route("CreateOrUpdate")]
        public void CreateOrUpdate(CreateOrEditRoleInputDto input)
        {
            _roleAppService.CreateOrUpdate(input);
        }
    }
}

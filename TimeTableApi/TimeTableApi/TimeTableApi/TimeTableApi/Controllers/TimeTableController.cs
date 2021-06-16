using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement.Dtos;
using TimeTableApi.Application.Shared.TimeTableManagement;
using TimeTableApi.Application.Shared.UserManagement;
using TimeTableApi.Application.Shared.UserManagement.Dtos;

namespace TimeTableApi.Controllers
{
    [Authorize]
    [Route("TimeTable")]
    public class TimeTableController : Controller
    {
        private readonly ITimeTableAppService _timeTableAppService;

        public TimeTableController(ITimeTableAppService timeTableAppService) {
            _timeTableAppService = timeTableAppService;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public TimeTableDto GetById(string id)
        {
            return _timeTableAppService.GetById(id);
        }

        [HttpPost]
        [Route("CreateOrUpdate")]
        public bool CreateOrUpdate([FromBody]CreateOrEditTimeTableInputDto input)
        {
            return _timeTableAppService.CreateOrUpdate(input);
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllByEventName/{eventName}")]
        public List<TimeTableDto> GetAllByEventName(string eventName)
        {
            return _timeTableAppService.GetAllByEventName(eventName);
        }


        [HttpGet]
        [Route("GetAllByEventId/{eventId}")]
        public List<TimeTableDto> GetAllByEventId(string eventId)
        {
            return _timeTableAppService.GetAllByEventId(eventId);
        }

        [HttpGet]
        [Route("schedule/GetAllByEventId/{eventId}")]
        public List<TimeTableDto> GetAllScheduleTimeTableByEventId(string eventId)
        {
            return _timeTableAppService.GetAllScheduleTimeTableByEventId(eventId);
        }


        [HttpDelete]
        [Route("delete/{id}")]
        public void Delete(string id)
        {
            _timeTableAppService.Remove(id);
        }
    }
}

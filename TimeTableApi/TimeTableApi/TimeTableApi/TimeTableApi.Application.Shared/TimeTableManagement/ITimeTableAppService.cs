using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement.Dtos;

namespace TimeTableApi.Application.Shared.TimeTableManagement
{
    public interface ITimeTableAppService
    {
        bool CreateOrUpdate(CreateOrEditTimeTableInputDto input);
        void Remove(string id);
        List<TimeTableDto> GetAllByEventName(string eventName);
        List<TimeTableDto> GetAllByEventId(string eventId);
        List<TimeTableDto> GetAllScheduleTimeTableByEventId(string eventId);
        TimeTableDto GetById(string id);
    }
}

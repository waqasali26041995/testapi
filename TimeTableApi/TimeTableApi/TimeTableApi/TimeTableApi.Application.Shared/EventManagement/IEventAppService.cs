using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement.Dtos;

namespace TimeTableApi.Application.Shared.EventManagement
{
    public interface IEventAppService
    {
        List<EventDto> GetAll(string userId);
        EventDto GetByName(string name);
        bool CreateOrUpdate(CreateOrEditEventInputDto input);
        void Remove(string id);
        EventDto GetById(string id);
    }
}

using System;
using TimeTableApi.Application.Shared.GenericDtos;

namespace TimeTableApi.Application.Shared.EventManagement.Dtos
{
    public class TimeTableDto : GenericDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string EventId { get; set; }
    }
}

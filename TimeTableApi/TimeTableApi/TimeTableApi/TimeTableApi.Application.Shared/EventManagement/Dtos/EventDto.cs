using System;
using System.Collections.Generic;
using System.Text;
using TimeTableApi.Application.Shared.GenericDtos;

namespace TimeTableApi.Application.Shared.EventManagement.Dtos
{
    public class EventDto : GenericDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public DateTime EventDate { get; set; }
        public string UserId { get; set; }
    }
}

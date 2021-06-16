using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TimeTableApi.Application.Shared.EventManagement.Dtos;
using TimeTableApi.Core.Entities;

namespace TimeTableApi.Application.AutoMapper
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, CreateOrEditEventInputDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
        }
    }
}

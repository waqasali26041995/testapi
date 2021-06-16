using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TimeTableApi.Application.Shared.EventManagement.Dtos;
using TimeTableApi.Core.Entities;

namespace TimeTableApi.Application.AutoMapper
{
    public class TimeTableProfile : Profile
    {
        public TimeTableProfile()
        {
            CreateMap <TimeTable, CreateOrEditTimeTableInputDto>().ReverseMap();
            CreateMap<TimeTable, TimeTableDto>().ReverseMap();
        }
    }
}

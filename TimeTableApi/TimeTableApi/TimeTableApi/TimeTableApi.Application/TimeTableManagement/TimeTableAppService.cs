using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement;
using TimeTableApi.Application.Shared.EventManagement.Dtos;
using TimeTableApi.Application.Shared.TimeTableManagement;
using TimeTableApi.Core.Entities;
using TimeTableApi.DB.Models;

namespace TimeTableApi.Application.EventManagement
{
    public class TimeTableAppService : ITimeTableAppService
    {
        private readonly IMongoCollection<TimeTable> _timeTable;
        private readonly IMapper _mapper;
        private readonly IEventAppService _eventAppService;
        public TimeTableAppService(
            IDatabaseSettings settings,
            IMapper mapper,
            IEventAppService eventAppService
            )
        {
            _mapper = mapper;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _timeTable = database.GetCollection<TimeTable>("EventTimeTable");

            _eventAppService = eventAppService;
        }
        public bool CreateOrUpdate(CreateOrEditTimeTableInputDto input)
        {
            try
            {
                var currDateTime = DateTime.Now;
                if (input.StartTime > currDateTime && input.EndTime > input.StartTime)
                {
                    if (!string.IsNullOrEmpty(input.Id))
                    {
                        Update(input);
                    }
                    else
                    {
                        Create(input);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        private void Create(CreateOrEditTimeTableInputDto input)
        {
            var createTimeTable = _mapper.Map<TimeTable>(input);

            _timeTable.InsertOne(createTimeTable);
        }

        private void Update(CreateOrEditTimeTableInputDto input)
        {
            var createTimeTable = _mapper.Map<TimeTable>(input);

            _timeTable.ReplaceOne(x => x.Id == createTimeTable.Id, createTimeTable);
        }

        public List<TimeTableDto> GetAllByEventId(string eventId)
        {
            var timeTables = _timeTable.Find(x => x.EventId == eventId).ToList();
            return _mapper.Map<List<TimeTableDto>>(timeTables);
        }


        public List<TimeTableDto> GetAllScheduleTimeTableByEventId(string eventId)
        {
            var currDate = DateTime.Now;
            var timeTables = _timeTable
                .Find(x => x.EventId == eventId)
                .SortBy(x => x.StartTime).ToList();
            timeTables = timeTables.Where(x => x.StartTime.Date == currDate.Date).ToList();
            return _mapper.Map<List<TimeTableDto>>(timeTables);
        }

        public TimeTableDto GetById(string id)
        {
            var timeTable = _timeTable.Find(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<TimeTableDto>(timeTable);
        }


        public List<TimeTableDto> GetAllByEventName(string eventName)
        {
            var eventByName = _eventAppService.GetByName(eventName);
            var events = _timeTable.Find(x => x.EventId == eventByName.Id).ToList();
            return _mapper.Map<List<TimeTableDto>>(events);
        }

        public void Remove(string id)
        {
            _timeTable.DeleteOne(x => x.Id == id);
        }
    }
}

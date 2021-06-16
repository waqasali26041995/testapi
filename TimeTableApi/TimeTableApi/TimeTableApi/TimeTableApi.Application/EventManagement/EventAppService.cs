using Abp.UI;
using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement;
using TimeTableApi.Application.Shared.EventManagement.Dtos;
using TimeTableApi.Core.Entities;
using TimeTableApi.DB.Models;

namespace TimeTableApi.Application.EventManagement
{
    public class EventAppService : IEventAppService
    {
        private readonly IMongoCollection<Event> _events;
        private readonly IMapper _mapper;
        public EventAppService(
            IDatabaseSettings settings,
            IMapper mapper
            )
        {
            _mapper = mapper;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _events = database.GetCollection<Event>("Events");
        }
        public bool CreateOrUpdate(CreateOrEditEventInputDto input)
        {
            try
            {
                if (string.IsNullOrEmpty(input.Id))
                {
                    Create(input);
                }
                else
                {
                    Update(input);
                }
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        private void Create(CreateOrEditEventInputDto input)
        {
            var createEvent = _mapper.Map<Event>(input);

            _events.InsertOne(createEvent);
        }

        private void Update(CreateOrEditEventInputDto input)
        {
            var createEvent = _mapper.Map<Event>(input);

            _events.ReplaceOne(x=> x.Id == createEvent.Id,createEvent);
        }

        public List<EventDto> GetAll(string userId)
        {
            var events = _events.Find(x=> x.UserId == userId).ToList();
            return _mapper.Map<List<EventDto>>(events);
        }

        public EventDto GetByName(string name)
        {
            var eventByName = _events.Find(x=> x.Name == name).FirstOrDefault();
            return _mapper.Map<EventDto>(eventByName);
        }

        public EventDto GetById(string id)
        {
            var eventById = _events.Find(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<EventDto>(eventById);
        }

        public void Remove(string id)
        {
            _events.DeleteOne(x => x.Id == id);
        }
    }
}

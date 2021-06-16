using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement;
using TimeTableApi.Application.Shared.EventManagement.Dtos;

namespace TimeTableApi.Controllers
{
    [Route("Event")]
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventAppService _eventAppService;

        public EventController(IEventAppService eventAppService) {
            _eventAppService = eventAppService;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<EventDto> GetAll()
        {
            var user = User.Claims.FirstOrDefault();
            return _eventAppService.GetAll(user.Value);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public EventDto GetById(string id)
        {
            return _eventAppService.GetById(id);
        }

        [HttpPost]
        [Route("CreateOrUpdate")]
        public bool CreateOrUpdate([FromBody] CreateOrEditEventInputDto input)
        {
            return _eventAppService.CreateOrUpdate(input);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public void Delte(string id)
        {
            _eventAppService.Remove(id);
        }

        [HttpPost]
        [Route("UploadEventImage")]
        public async Task UploadEventImage([FromForm] ImageDto input)
        {
            if (input == null)
                return;

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/content",
                        input.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await input.Image.CopyToAsync(stream);
            }
            return;
        }
    }
}

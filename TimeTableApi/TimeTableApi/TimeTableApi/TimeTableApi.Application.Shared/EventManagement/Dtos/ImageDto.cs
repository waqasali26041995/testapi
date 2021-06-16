using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TimeTableApi.Application.Shared.GenericDtos;

namespace TimeTableApi.Application.Shared.EventManagement.Dtos
{
    public class ImageDto
    {
        public string FileName { get; set; }

        public IFormFile Image { get; set; }
    }
}

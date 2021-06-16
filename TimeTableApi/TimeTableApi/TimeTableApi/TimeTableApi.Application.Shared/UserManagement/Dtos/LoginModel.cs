using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableApi.Application.Shared.UserManagement.Dtos
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

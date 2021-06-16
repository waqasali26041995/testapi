using System;
using TimeTableApi.Application.Shared.GenericDtos;

namespace TimeTableApi.Application.Shared.UserManagement.Dtos
{
    public class CreateOrEditUserInputDto : GenericDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string RoleId { get; set; }
    }
}

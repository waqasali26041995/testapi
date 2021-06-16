using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement;
using TimeTableApi.Application.Shared.EventManagement.Dtos;
using TimeTableApi.Application.Shared.RoleManagement;
using TimeTableApi.Application.Shared.RoleManagement.Dtos;
using TimeTableApi.Application.Shared.UserManagement;
using TimeTableApi.Application.Shared.UserManagement.Dtos;
using TimeTableApi.Core.Entities;
using TimeTableApi.DB.Models;

namespace TimeTableApi.Application.RoleManagement
{
    public class RoleAppService : IRoleAppService
    {
        private readonly IMongoCollection<Role> _roles;
        private readonly IMapper _mapper;
        public RoleAppService(
            IDatabaseSettings settings,
            IMapper mapper
            )
        {
            _mapper = mapper;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _roles = database.GetCollection<Role>("Roles");
        }
        public void CreateOrUpdate(CreateOrEditRoleInputDto input)
        {
            if (string.IsNullOrEmpty(input.Id))
            {
                Create(input);
            }
            else
            {
                Update(input);
            }
        }

        private void Create(CreateOrEditRoleInputDto input)
        {
            var role = _mapper.Map<Role>(input);

            _roles.InsertOne(role);
        }

        private void Update(CreateOrEditRoleInputDto input)
        {
            var role = _mapper.Map<Role>(input);

            _roles.ReplaceOne(x=> x.Id == role.Id, role);
        }

        public void Remove(string id)
        {
            _roles.DeleteOne(x => x.Id == id);
        }

        public List<RoleDto> GetAll()
        {
            var roles = _roles.Find(role => true).ToList();
            return _mapper.Map<List<RoleDto>>(roles);
        }


        public RoleDto GetById(string id)
        {
            var role = _roles.Find(x=> x.Id == id).FirstOrDefault();
            return _mapper.Map<RoleDto>(role);
        }


        public RoleDto GetByName(string name)
        {
            var role = _roles.Find(x => x.Name == name).FirstOrDefault();
            return _mapper.Map<RoleDto>(role);
        }
    }
}

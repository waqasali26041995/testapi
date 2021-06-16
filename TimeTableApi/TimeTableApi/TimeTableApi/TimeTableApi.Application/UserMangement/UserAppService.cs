using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TimeTableApi.Application.Shared.EventManagement;
using TimeTableApi.Application.Shared.EventManagement.Dtos;
using TimeTableApi.Application.Shared.RoleManagement;
using TimeTableApi.Application.Shared.UserManagement;
using TimeTableApi.Application.Shared.UserManagement.Dtos;
using TimeTableApi.Core.Entities;
using TimeTableApi.DB.Models;

namespace TimeTableApi.Application.UserManagement
{
    public class UserAppService : IUserAppService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMapper _mapper;
        private readonly IRoleAppService _roleAppService;
        public UserAppService(
            IDatabaseSettings settings,
            IRoleAppService roleAppService,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _roleAppService = roleAppService;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>("Users");
        }
        public void CreateOrUpdate(CreateOrEditUserInputDto input)
        {
            var role = _roleAppService.GetByName("User");
            if (role != null)
            {
                input.RoleId = role.Id;
            }

            if (string.IsNullOrEmpty(input.Id))
            {
                    Create(input);
            }
            else
            {
                Update(input);
            }
        }

        private void Create(CreateOrEditUserInputDto input)
        {
            var createUser = _mapper.Map<User>(input);

            createUser.Password = HashPassword(input.Password);

            _users.InsertOne(createUser);
        }

        private void Update(CreateOrEditUserInputDto input)
        {
            var createUser = _mapper.Map<User>(input);
            var user = GetById(input.Id);
            if (user != null)
            {
                createUser.Password = user.Password;
            }
            _users.ReplaceOne(x=> x.Id == createUser.Id, createUser);
        }

        public List<UserDto> GetAll()
        {
            var role =_roleAppService.GetByName("User");
            if (role != null)
            {
                var users = _users.Find(x => x.RoleId == role.Id).ToList();
                return _mapper.Map<List<UserDto>>(users);
            }
            return new List<UserDto>();
        }

        public UserDto GetById(string id)
        {
            var users = _users.Find(x=> x.Id == id).FirstOrDefault();
            return _mapper.Map<UserDto>(users);
        }

        public UserDto GetByEmail(LoginModel input)
        {
            try
            {
                if (input != null)
                {
                    var userByEmail = _users.Find(x => x.Email == input.Email).FirstOrDefault();
                    if (userByEmail != null)
                    {
                        var hashPassword = PlainPassword(input.Password, userByEmail.Password);
                        if (hashPassword)
                        {
                            return _mapper.Map<UserDto>(userByEmail);
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            { 
                
            }
            return null;
        }

        public void Remove(string id)
        {
            _users.DeleteOne(x => x.Id == id);
        }

        private string HashPassword(string password)
        {

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            //byte[] salt = new byte[128 / 8];
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(salt);
            //}
            //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            //// derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            //string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            //    password: password,
            //    salt: salt,
            //    prf: KeyDerivationPrf.HMACSHA1,
            //    iterationCount: 10000,
            //    numBytesRequested: 256 / 8));

            return savedPasswordHash;
        }


        private bool PlainPassword(string password, string savedPasswordHash)
        {
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

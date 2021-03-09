using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
using WebService.Core.Interfaces.MongoDBInterfaces;

namespace WebService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserMongoDBContext _userMongoDBContext;
        public UserRepository(IUserMongoDBContext userMongoDBContext)
        {
            _userMongoDBContext = userMongoDBContext;
        }

        public Task<UserDTO> CreateUser(UserDTO user)
        {
            return _userMongoDBContext.CreateUser(user);
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            return await _userMongoDBContext.GetUsersByFilters(new JObject { new JProperty("All", "")});
        }

        public async Task<UserDTO> GetUserById(string id)
        {
            var filter = new JObject
            {
                new JProperty("Id", id.ToString())
            };
            var users = await _userMongoDBContext.GetUsersByFilters(filter);
            return users.FirstOrDefault();
        }

        public async Task<List<UserDTO>> GetAllUsersByPlaceId(string id)
        {
            var filter = new JObject
            {
                new JProperty("PlaceId", id)
            };
            return await _userMongoDBContext.GetUsersByFilters(filter);
        }

        public async Task<UserDTO> GetUserByIdentification(string identification)
        {
            var filter = new JObject
            {
                new JProperty("Identification", identification)
            };
            var users = await _userMongoDBContext.GetUsersByFilters(filter);
            return users.FirstOrDefault();
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var filter = new JObject
            {
                new JProperty("Email", email)
            };
            var users = await _userMongoDBContext.GetUsersByFilters(filter);
            return users.FirstOrDefault();
        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            return await _userMongoDBContext.UpdateUser(user);
        }

        public async Task<UserDTO> DeleteUserById(string id)
        {
            var userDeleted = await GetUserById(id);
            await _userMongoDBContext.DeleteUserById(id);
            return userDeleted;
        }
    }
}

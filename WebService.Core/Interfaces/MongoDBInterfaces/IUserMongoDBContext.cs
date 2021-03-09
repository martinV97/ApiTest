using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces.MongoDBInterfaces
{
    public interface IUserMongoDBContext
    {
        Task<UserDTO> CreateUser(UserDTO user);
        Task<List<UserDTO>> GetUsersByFilters(JToken filters);
        Task<UserDTO> UpdateUser(UserDTO user);
        Task DeleteUserById(string id);
    }
}

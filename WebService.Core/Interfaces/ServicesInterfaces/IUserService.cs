using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(User User);
        Task<List<UserDTO>> GetAllUsers();
        Task<List<UserDTO>> GetAllUsersByPlaceId(string id);
        Task<UserDTO> GetUserById(string id);
        Task<UserDTO> GetUserByIdentification(string identification);
        Task<UserDTO> GetUserByEmail(string email);
        Task<UserDTO> UpdateUser(UserDTO user);
        Task<UserDTO> DeleteUserById(string id);
    }
}

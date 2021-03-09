using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces
{
    public interface IUserRepository
    {

        Task<UserDTO> CreateUser(UserDTO user);
        Task<List<UserDTO>> GetAllUsers();
        Task<List<UserDTO>> GetAllUsersByPlaceId(string id);
        Task<UserDTO> GetUserById(string id);
        Task<UserDTO> GetUserByIdentification(string identification);
        Task<UserDTO> GetUserByEmail(string email);
        Task<UserDTO> UpdateUser(UserDTO user);
        Task<UserDTO> DeleteUserById(string id);
    }
}

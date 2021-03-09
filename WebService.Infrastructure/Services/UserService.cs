using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;
using WebService.Core.Interfaces.ServicesInterfaces;

namespace WebService.Infrastructure.Repositories
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IPlaceService _placeService;
        public UserService(IUserRepository userRepository, IPasswordService passwordService, IPlaceService placeService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _placeService = placeService;
        }

        public async Task<UserDTO> CreateUser(User user)
        {
            if (!await ValidateUserEmail(user.Email))
                throw new ArgumentException($"An user identified Email: {user.Email} already exist");
            if(!ValidateIsAdult(user.BirthDate))
                throw new ArgumentException($"The user must be of legal age: {user.BirthDate}");
            if(await _placeService.GetPlaceById(user.PlaceId) is null)
                throw new ArgumentNullException($"The place: {user.PlaceId} it doesn't exist");
            user.Password = _passwordService.Hash(user.Password);
            var userDto = user.Adapt<UserDTO>();
            var result = await _userRepository.CreateUser(userDto);
            return result;
        }

        private async Task<bool> ValidateUserEmail(string email)
        {
            if (await _userRepository.GetUserByEmail(email) is null)
                return true;
            return false;
        }

        private bool ValidateIsAdult(DateTime birthDate)
        {
            if (birthDate.AddYears(18) <= DateTime.Today)
                return true;
            return false;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<UserDTO> GetUserById(string id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<List<UserDTO>> GetAllUsersByPlaceId(string id)
        {
            return await _userRepository.GetAllUsersByPlaceId(id);
        }

        public async Task<UserDTO> GetUserByIdentification(string identification)
        {
            return await _userRepository.GetUserByIdentification(identification);
        }

        public async Task<UserDTO> GetUserByEmail(string email) 
        {
            return await _userRepository.GetUserByEmail(email);
        }


        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            if (!await ValidateUserEmail(user.Email))
                throw new ArgumentException($"An user identified Email: {user.Email} already exist");
            if (!ValidateIsAdult(user.BirthDate))
                throw new ArgumentException($"The user must be of legal age: {user.BirthDate}");
            user.Password = _passwordService.Hash(user.Password);
            return await _userRepository.UpdateUser(user);
        }

        public async Task<UserDTO> DeleteUserById(string id)
        {
            return await _userRepository.DeleteUserById(id);
        }
    }
}

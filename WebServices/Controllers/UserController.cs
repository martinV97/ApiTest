using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///Creates a User
        /// </summary>
        /// <param name="user">User object to create</param>
        /// <returns>User object created with Id</returns>
        [HttpPost]
        [Route("createUser")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<IActionResult> CreatePayment(User user)
        {
            var result = await _userService.CreateUser(user);
            return Ok(result);
        }

        /// <summary>
        ///Get the complet list of users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [Route("getAllUsers")]
        [ProducesResponseType(typeof(List<UserDTO>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        /// <summary>
        ///Get a list of user by placeId
        /// </summary>
        /// <param name="id">Place id</param>
        /// <returns>List of users</returns>
        [HttpGet]
        [Route("getAllUsersByPlaceId")]
        [ProducesResponseType(typeof(List<UserDTO>), 200)]
        public async Task<IActionResult> GetAllUsersByPlaceId(string id)
        {
            var result = await _userService.GetAllUsersByPlaceId(id);
            return Ok(result);
        }

        /// <summary>
        ///Get a user by Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User object</returns>
        [HttpGet]
        [Route("getUserById")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }

        /// <summary>
        ///Get a user by Identification
        /// </summary>
        /// <param name="identification">User identification</param>
        /// <returns>User object</returns>
        [HttpGet]
        [Route("getUserByIdentification")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<IActionResult> GetUserByIdentification(string identification)
        {
            var result = await _userService.GetUserByIdentification(identification);
            return Ok(result);
        }

        /// <summary>
        ///Get a user by Email
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User object</returns>
        [HttpGet]
        [Route("getUserByEmail")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmail(email);
            return Ok(result);
        }

        /// <summary>
        ///Update a user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>Updated user object</returns>
        [HttpPut]
        [Route("updateUser")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<IActionResult> UpdateUser(UserDTO user)
        {
            var result = await _userService.UpdateUser(user);
            return Ok(result);
        }

        /// <summary>
        ///Delete a user by Id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Deleted user object</returns>
        [HttpDelete]
        [Route("deleteUserById")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            var result = await _userService.DeleteUserById(id);
            return Ok(result);
        }
    }
}

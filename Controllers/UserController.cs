using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerServer.Models;
using MessengerServer.Repositories;

namespace MessengerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        // {
        //     var users = await _userRepository.GetAllUsersAsync();
        //     return Ok(users);
        // }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<User>> GetUserById(Guid id)
        // {
        //     var user = await _userRepository.GetUserByIdAsync(id);

        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(user);
        // }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if(user == null) return BadRequest("Input is null!");
            if(_userRepository.UserValid(user)) return BadRequest("User alredy exist!");
            var createdUser = await _userRepository.CreateUserAsync(user);
            if (createdUser == null)
            {
                return BadRequest("Error while creating user!");
            }
            else return createdUser;
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            if (user.Email == null || user.Password == null) return BadRequest("Input user is null!");
            if(!_userRepository.UserValid(user)) return BadRequest("Invalid user login or password!");

            user = await _userRepository.UpdateUserAsync(user);

            if (user == null)
            {
                return BadRequest("Error while updating user!");
            }
            else return user;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(User user)
        {
            if(!_userRepository.UserValid(user)) return BadRequest("Invalid user login or password!");
            if (!_userRepository.UserExists(user.Id)) return NotFound();

            await _userRepository.DeleteUserAsync(user.Id);

            if (!_userRepository.UserExists(user.Id)) return Ok();
            else return BadRequest("Error while deleting user!");
        }
    }
}
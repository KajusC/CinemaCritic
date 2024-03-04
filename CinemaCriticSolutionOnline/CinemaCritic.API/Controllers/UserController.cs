using AutoMapper;
using CinemaCritic.API.Dto;
using CinemaCritic.API.Models;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCritic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<User>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetAllUsers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUser(int userId)
        {
            if(!await _userRepository.UserExists(userId))
            {
                return BadRequest("User does not exist");
            }
            var user = _mapper.Map<UserDto>(await _userRepository.GetUser(userId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
            {
                return BadRequest(ModelState);
            }
            var users = await _userRepository.GetAllUsers();

            var user = users.Where(c => c.Email.Trim().ToUpper() == userCreate.Email.TrimEnd().ToUpper()).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User already exits");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user1 = new User()
            {
                FirstName = userCreate.FirstName,
                LastName = userCreate.LastName,
                Email = userCreate.Email,
                Password = userCreate.Password,
                Username = userCreate.Username
            };

            if (!await _userRepository.CreateUser(user1))
            {
                ModelState.AddModelError("", "This");
                return StatusCode(500, ModelState);
            }
            return Ok("Success");
        }
        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userToUpdate)
        {
            if (userToUpdate is null)
            {
                return BadRequest(ModelState);
            }
            if(userToUpdate.Id != userId)
            {
                return BadRequest(ModelState);
            }
            if(!await _userRepository.UserExists(userId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ownerMap = _mapper.Map<User>(userToUpdate);
            if (!await _userRepository.UpdateUser(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (!await _userRepository.UserExists(userId))
            {
                return NotFound();
            }
            var user = await _userRepository.GetUser(userId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

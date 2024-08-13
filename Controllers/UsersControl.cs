using Microsoft.AspNetCore.Mvc;
using test_back.Services;
using test_back.Models;

namespace test_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersControl : ControllerBase
    {
        private readonly IUserService UserService;

        public UsersControl(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await UserService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await UserService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            await UserService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await UserService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await UserService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await UserService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}

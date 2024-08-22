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
        // POST: api/User/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserService.RegisterUserAsync(model);
            if (result)
                return Ok(new { message = "User registered successfully" });

            return BadRequest(new { message = "User registration failed" });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserService.LoginUserAsync(model);
            if (result)
                return Ok(new { message = "User logged in successfully" });

            return Unauthorized(new { message = "Invalid username or password" });
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await UserService.LogoutUserAsync();
            return Ok(new { message = "User logged out successfully" });
        }

        // GET: api/User/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await UserService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "Пользователь не найден" });

            return Ok(user);
        }
        [HttpGet("{userId}/Roles")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var user = await UserService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var roles = await UserService.GetUserRolesAsync(user);
            return Ok(roles);
        }
    }
}

using Items.Dto;
using Items.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Items.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UsersRepo _usersRepo;
        private readonly TokenService _tokenService;

        public AuthController(UsersRepo usersRepo, TokenService tokenService)
        {
            _usersRepo = usersRepo;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = _usersRepo.GetUser(registerDto.Email);
            if (user != null)
            {
                return BadRequest("Email already in use");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            _usersRepo.AddUser(new Models.User { Email = registerDto.Email, PasswordHash = hashedPassword });

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = _usersRepo.GetUser(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(user.Email);

            return Ok(new { User = user, Token = token.token, Expires_at = token.expires_at });
        }
    }
}

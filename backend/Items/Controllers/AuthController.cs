using Items.Dto;
using Items.MessageResult;
using Items.Messages;
using Items.Repos;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Items.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UsersRepo _usersRepo;
        private readonly TokenService _tokenService;
        IRequestClient<RegisterMessage> _registerClient;

        public AuthController(UsersRepo usersRepo, TokenService tokenService, IRequestClient<RegisterMessage> registerClient)
        {
            _usersRepo = usersRepo;
            _tokenService = tokenService;
            _registerClient = registerClient;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterMessage registerMsg)
        {

            Response response = await _registerClient.GetResponse<RegisterResult, RegisterFail>(registerMsg);
            switch (response)
            {
                case (_, RegisterResult result) responseA:
                    return Ok(result);
                case (_, RegisterFail Fail) responseB:
                    return BadRequest("Email of password is incorrect");
                default:
                    throw new InvalidOperationException();

            }

            //var userInDb = _usersRepo.GetUser(registerDto.Email);
            //if (userInDb != null)
            //{
            //    return BadRequest("Email already in use");
            //}

            //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            //var user = new User { Email = registerDto.Email, PasswordHash = hashedPassword };
            //_usersRepo.AddUser(user);

            //var token = _tokenService.GenerateToken(user.Email);

            //return Ok(new { User = user, Token = token.token, Expires_at = token.expires_at });
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

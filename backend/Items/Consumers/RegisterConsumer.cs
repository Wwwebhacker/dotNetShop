using Items.MessageResult;
using Items.Messages;
using Items.Models;
using Items.Repos;
using MassTransit;

namespace Items.Consumers
{
    public class RegisterConsumer : IConsumer<RegisterMessage>
    {

        private readonly UsersRepo _usersRepo;
        private readonly TokenService _tokenService;


        public RegisterConsumer(UsersRepo usersRepo, TokenService tokenService)
        {
            _usersRepo = usersRepo;
            _tokenService = tokenService;
        }


        public async Task Consume(ConsumeContext<RegisterMessage> context)
        {
            var userInDb = _usersRepo.GetUser(context.Message.Email);
            if (userInDb != null)
            {
                await context.RespondAsync<RegisterFail>(context.Message);
                return;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(context.Message.Password);
            var user = new User { Email = context.Message.Email, PasswordHash = hashedPassword };
            _usersRepo.AddUser(user);

            var token = _tokenService.GenerateToken(user.Email);

            await context.RespondAsync<RegisterResult>(new RegisterResult
            {
                Token = token.token,
                User = user,
                Expires_at = token.expires_at
            });
        }
    }
}

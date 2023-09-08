using Items.Models;
using Items.Repos;
using System.IdentityModel.Tokens.Jwt;

namespace Items
{
    public class HttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsersRepo _usersRepo;
        public HttpContextService(IHttpContextAccessor httpContextAccessor, UsersRepo usersRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _usersRepo = usersRepo;
        }

        public User getUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null.");
            }

            var user = httpContext.User;

            var email = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidOperationException("Email claim is missing or null.");
            }

            return _usersRepo.GetUser(email);
        }
    }
}

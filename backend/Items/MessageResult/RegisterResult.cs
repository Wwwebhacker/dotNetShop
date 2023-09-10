using Items.Models;

namespace Items.MessageResult
{
    public record RegisterResult
    {
        public User User { get; init; }
        public string Token { get; init; }
        public DateTime Expires_at { get; init; }
    }
}

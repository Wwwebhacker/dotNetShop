namespace Items.Messages
{
    public record RegisterMessage
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}

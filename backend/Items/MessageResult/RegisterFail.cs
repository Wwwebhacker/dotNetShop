using Items.Messages;

namespace Items.MessageResult
{
    public record RegisterFail
    {
        public RegisterMessage registerMessage { get; init; }
    }
}

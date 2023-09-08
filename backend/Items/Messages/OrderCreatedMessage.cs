using Items.Models;

namespace Items.Messages
{
    public class OrderCreatedMessage
    {
        public User user { get; init; }
        public List<int> productIds { get; init; }
    }
}

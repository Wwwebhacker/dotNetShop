namespace Items.Messages
{
    public record ItemCreatedMessage
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}

namespace Items.Messages
{
    public record ProductCreatedMessage
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}

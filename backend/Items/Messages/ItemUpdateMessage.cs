namespace Items.Messages
{
    public record ItemUpdateMessage
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}

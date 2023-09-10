namespace Items.Messages
{
    public record ProductCreatedMessage
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public int InventoryCount { get; init; }

        public decimal Price { get; init; }
    }
}

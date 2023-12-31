﻿namespace Items.Messages
{
    public record ProductUpdateMessage
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public int InventoryCount { get; init; }
        public string ImageUrl { get; init; }
    }
}

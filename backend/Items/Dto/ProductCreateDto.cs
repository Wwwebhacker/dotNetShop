namespace Items.Dto
{
    public record ProductCreateDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public int InventoryCount { get; init; }
        public IFormFile Image { get; init; }
    }
}

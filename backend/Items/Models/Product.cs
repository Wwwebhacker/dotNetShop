namespace Items.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public required int InventoryCount { get; set; }
        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}

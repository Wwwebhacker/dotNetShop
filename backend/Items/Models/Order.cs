namespace Items.Models
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<Item> Items { get; } = new List<Item>();
    }
}

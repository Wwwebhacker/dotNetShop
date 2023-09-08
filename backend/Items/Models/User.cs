namespace Items.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}

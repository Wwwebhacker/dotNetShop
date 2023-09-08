using Items.Models;

namespace Items.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=appDatabase.db");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Order>()
        //        .HasMany(x => x.Products)
        //        .WithMany(y => y.Orders)
        //        .UsingEntity(j => j.ToTable("OrderProduct"));

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}

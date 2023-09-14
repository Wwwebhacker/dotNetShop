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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "Fender hsh",
                    Description = "color: sea",
                    Price = new decimal(1122.0),
                    ImageUrl =$"/images/hsh-sea.jpg",
                    InventoryCount = 10
                },
                new Product {
                    Id = 2,
                    Name = "Fender hss",
                    Description = "color: flame top",
                    Price = new decimal(850.0),
                    ImageUrl =$"/images/hss-ft.jpg",
                    InventoryCount = 10
                },
                new Product {
                    Id = 3,
                    Name = "Fender sss left",
                    Description = "color: black",
                    Price = new decimal(850.0),
                    ImageUrl =$"/images/sss-lf.jpg",
                    InventoryCount = 10
                },
                new Product {
                    Id = 4,
                    Name = "Fender sss",
                    Description = "color: yellow",
                    Price = new decimal(850.0),
                    ImageUrl =$"/images/sss-yl.jpg",
                    InventoryCount = 10
                },
            };


            modelBuilder.Entity<Product>().HasData(products);
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

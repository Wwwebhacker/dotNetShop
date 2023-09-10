using Items.Data;
using Items.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Items.Repos
{
    public class EfProductRepo
    {
        private readonly ApplicationDbContext _context;

        public EfProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
        public List<Product> GetProducts() => _context.Products.ToList();

        public Product GetProduct(int id) => _context.Products.FirstOrDefault(i => i.Id == id);

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Attach(product);

            var entry = _context.Entry(product);
            var primaryKey = _context.Model.FindEntityType(typeof(Product)).FindPrimaryKey().Properties.Single();

            foreach (var property in entry.OriginalValues.Properties)
            {
                if (property == primaryKey)
                    continue;

                var current = entry.CurrentValues[property];

                if (current == null)
                {
                    entry.Property(property.Name).IsModified = false;
                }
                else
                {
                    entry.Property(property.Name).IsModified = true;
                }
            }

            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(i => i.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}

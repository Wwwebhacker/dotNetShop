using Items.Data;
using Items.Models;

namespace Items.Repos
{
    public class EfProductRepo
    {
        private readonly ApplicationDbContext _context;

        public EfProductRepo(ApplicationDbContext context)
        {
            _context = context;
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
            _context.Entry(product).State = EntityState.Modified;
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

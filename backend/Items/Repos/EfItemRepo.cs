using Items.Data;
using Items.Models;

namespace Items.Repos
{
    public class EfItemRepo : IItemRepo
    {
        private readonly ApplicationDbContext _context;

        public EfItemRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Item> GetItems() => _context.Items.ToList();

        public Item GetItem(int id) => _context.Items.FirstOrDefault(i => i.Id == id);

        public void AddItem(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}

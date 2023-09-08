using Items.Data;
using Items.Models;

namespace Items.Repos
{
    public class OrderRepo
    {
        private readonly ApplicationDbContext context;

        public OrderRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Order> GetOrders(User user)
        {
            //return context.Orders.Where(o => o.UserId == user.Id).Include(o => o.Products).ToList();

            var orderDTOs = context.Orders
                       .Where(o => o.UserId == user.Id)
                       .Select(o => new Order
                       {
                           Id = o.Id,
                           Products = o.Products.Select(p => new Product
                           {
                               Id = p.Id,
                               Name = p.Name,
                               Description = p.Description
                           }).ToList()
                       }).ToList();
            return orderDTOs;
        }

        public void AddOrder(User user, List<int> productIds)
        {
            var products = context.Products.Where(i => productIds.Contains(i.Id)).ToList();

            if (products.Count != productIds.Count)
            {
                throw new InvalidOperationException("Some products could not be found.");
            }

            var order = new Order();
            order.UserId = user.Id;
            foreach (var product in products)
            {
                order.Products.Add(product);
            }

            context.Orders.Add(order);
            context.SaveChanges();
        }

    }
}

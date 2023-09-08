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
        public void AddOrder(List<int> itemIds)
        {
            var items = context.Items.Where(i => itemIds.Contains(i.Id)).ToList();

            if (items.Count != itemIds.Count)
            {
                throw new InvalidOperationException("Some items could not be found.");
            }

            var order = new Order();
            foreach (var item in items)
            {
                order.Items.Add(item);
            }

            context.Orders.Add(order);
            context.SaveChanges();
        }

    }
}

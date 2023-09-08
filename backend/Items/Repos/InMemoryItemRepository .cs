using Items.Models;

namespace Items.Repos
{
    public class InMemoryItemRepository : IItemRepo
    {
        private readonly List<Item> items = new List<Item>();

        public List<Item> GetItems() => items;

        public Item GetItem(int id) => items.FirstOrDefault(i => i.Id == id);

        public void AddItem(Item item) => items.Add(item);

        public void UpdateItem(Item item)
        {
            var index = items.FindIndex(i => i.Id == item.Id);
            if (index != -1) items[index] = item;
        }

        public void DeleteItem(int id) => items.RemoveAll(i => i.Id == id);
    }
}

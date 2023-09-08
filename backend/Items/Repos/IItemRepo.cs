using Items.Models;

namespace Items.Repos
{
    public interface IItemRepo
    {
        List<Item> GetItems();
        Item GetItem(int id);
        void AddItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(int id);
    }
}

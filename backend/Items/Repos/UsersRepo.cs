using Items.Data;
using Items.Models;

namespace Items.Repos
{
    public class UsersRepo
    {
        private readonly ApplicationDbContext context;

        public UsersRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public User GetUser(int id)
        {
            return context.Users.FirstOrDefault(i => i.Id == id);
        }

        public User GetUser(string email)
        {
            return context.Users.FirstOrDefault(i => i.Email == email);
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}

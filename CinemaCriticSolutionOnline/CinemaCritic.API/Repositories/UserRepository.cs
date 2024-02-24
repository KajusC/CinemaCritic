using CinemaCritic.API.Data;
using CinemaCritic.API.Models;
using CinemaCritic.API.Repositories.Contracts;

namespace CinemaCritic.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return SaveChanges();
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return SaveChanges();
        }

        public ICollection<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return SaveChanges();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(x => x.Id == id);
        }
    }
}

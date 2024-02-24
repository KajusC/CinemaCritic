using CinemaCritic.API.Models;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IUserRepository
    {
        ICollection<User> GetAllUsers();
        User GetUser(int id);
        //if needed add more methods
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool UserExists(int id);
        bool SaveChanges();
    }
}

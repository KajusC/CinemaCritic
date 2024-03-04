using CinemaCritic.API.Models;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllUsers();
        Task<User> GetUser(int id);
        //if needed add more methods
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> UserExists(int id);
        Task<bool> SaveChanges();
    }
}

using CinemaCritic.API.Data;
using CinemaCritic.API.Models;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CinemaCritic.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            return await SaveChanges();
        }

        public async Task<bool> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return await SaveChanges();
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUser(string username)
        {
            return await _context.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Users.Update(user);
            return await SaveChanges();
        }

        public async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(x => x.Id == id);
        }
        public async Task<bool> UserExists(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName);
        }
    }
}

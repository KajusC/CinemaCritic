using CinemaCritic.API.Data;
using CinemaCritic.API.Models;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaCritic.API.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;
        public GenreRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateGenre(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            return await SaveChanges();
        }

        public async Task<bool> DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            return await SaveChanges();
        }

        public async Task<bool> GenreExists(int id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }

        public async Task<ICollection<Genre>> GetAllGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetGenre(int id)
        {
            return await _context.Genres.Where(g => g.Id == id).FirstOrDefaultAsync();
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

        public async Task<bool> UpdateGenre(Genre genre)
        {
            _context.Genres.Update(genre);
            return await SaveChanges();
        }
    }
}

using CinemaCritic.API.Data;
using CinemaCritic.API.Models;
using CinemaCritic.API.Models.JoinTables;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaCritic.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;
        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            return await SaveChanges();
        }

        public async Task<bool> DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
            return await SaveChanges();
        }

        public async Task<ICollection<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovie(int id)
        {
            return await _context.Movies.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> MovieExists(int id)
        {
            return await _context.Movies.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            return await SaveChanges();
        }
    }
}

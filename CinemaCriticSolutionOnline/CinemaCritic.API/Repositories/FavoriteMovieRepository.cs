using CinemaCritic.API.Data;
using CinemaCritic.API.Models;
using CinemaCritic.API.Models.JoinTables;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaCritic.API.Repositories
{
    public class FavoriteMovieRepository : IFavoriteMovieRepository
    {
        private readonly DataContext _context;
        public FavoriteMovieRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateFavoriteMovie(int userId, int movieId)
        {
            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var movie = await _context.Movies.Where(m => m.Id == movieId).FirstOrDefaultAsync();

            if(user == null || movie == null)
                return false;

            var favoriteMovie = new UserMovies()
            {
                User = user,
                Movie = movie
            }; 
            await _context.UserMovies.AddAsync(favoriteMovie);
            return await SaveChanges();
        }

        public async Task<bool> DeleteFavoriteMovie(int userId, int movieId)
        {
            var favoriteMovie = await _context.UserMovies.Where(um => um.UserId == userId && um.MovieId == movieId).FirstOrDefaultAsync();

            if(favoriteMovie == null)
                return false;

            _context.UserMovies.Remove(favoriteMovie);
            return await SaveChanges();
        }

        public async Task<bool> FavoriteMovieExists(int userId, int movieId)
        {
            return await _context.UserMovies.AnyAsync(um => um.UserId == userId && um.MovieId == movieId);
        }

        public async Task<ICollection<Movie>> GetAllFavoriteMoviesOfUser(int userId)
        {
            return await _context.UserMovies.Where(um => um.UserId == userId).Select(um => um.Movie).ToListAsync();
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
    }
}

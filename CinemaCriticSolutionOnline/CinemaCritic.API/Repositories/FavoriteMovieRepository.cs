using CinemaCritic.API.Data;
using CinemaCritic.API.Models;
using CinemaCritic.API.Models.JoinTables;
using CinemaCritic.API.Repositories.Contracts;

namespace CinemaCritic.API.Repositories
{
    public class FavoriteMovieRepository : IFavoriteMovieRepository
    {
        private readonly DataContext _context;
        public FavoriteMovieRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFavoriteMovie(int userId, int movieId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            var movie = _context.Movies.Where(m => m.Id == movieId).FirstOrDefault();

            var favoriteMovie = new UserMovies()
            {
                User = user,
                Movie = movie
            }; 
            _context.UserMovies.Add(favoriteMovie);
            return SaveChanges();
        }

        public bool DeleteFavoriteMovie(int userId, int movieId)
        {
            _context.UserMovies.Remove(_context.UserMovies.Where(um => um.UserId == userId && um.MovieId == movieId).FirstOrDefault());
            return SaveChanges();
        }

        public bool FavoriteMovieExists(int userId, int movieId)
        {
            return _context.UserMovies.Any(um => um.UserId == userId && um.MovieId == movieId);
        }

        public ICollection<Movie> GetAllFavoriteMoviesOfUser(int userId)
        {
            return _context.UserMovies.Where(um => um.UserId == userId).Select(um => um.Movie).ToList();
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
    }
}

using CinemaCritic.API.Data;
using CinemaCritic.API.Models;
using CinemaCritic.API.Models.JoinTables;
using CinemaCritic.API.Repositories.Contracts;

namespace CinemaCritic.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;
        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            return SaveChanges();
        }

        public bool DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
            return SaveChanges();
        }

        public ICollection<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        public Movie GetMovie(int id)
        {
            return _context.Movies.Where(m => m.Id == id).FirstOrDefault();
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            return SaveChanges();
        }
    }
}

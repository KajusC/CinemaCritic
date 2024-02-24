using CinemaCritic.API.Models;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetAllMovies();
        Movie GetMovie(int id);
        //if needed add more methods
        bool CreateMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        bool DeleteMovie(Movie movie);
        bool MovieExists(int id);
        bool SaveChanges();
    }
}

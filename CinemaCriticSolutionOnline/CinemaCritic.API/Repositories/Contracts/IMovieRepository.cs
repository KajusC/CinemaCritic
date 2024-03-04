using CinemaCritic.API.Models;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> GetAllMovies();
        Task<Movie> GetMovie(int id);
        //if needed add more methods
        Task<bool> CreateMovie(Movie movie);
        Task<bool> UpdateMovie(Movie movie);
        Task<bool> DeleteMovie(Movie movie);
        Task<bool> MovieExists(int id);
        Task<bool> SaveChanges();
    }
}

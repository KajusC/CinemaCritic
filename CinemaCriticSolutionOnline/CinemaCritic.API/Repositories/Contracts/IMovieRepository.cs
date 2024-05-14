using CinemaCritic.API.Models;
using CinemaCritic.Models.Dto;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> GetAllMovies();
        Task<ICollection<TopMoviesDto>> GetTopMovies();
        Task<Movie> GetMovie(int id);
        Task<Movie> GetMovieDetails(int id);
        Task<bool> CreateMovie(Movie movie);
        Task<bool> UpdateMovie(Movie movie);
        Task<bool> DeleteMovie(Movie movie);
        Task<bool> MovieExists(int id);
        Task<bool> SaveChanges();
    }
}

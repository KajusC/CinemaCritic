using CinemaCritic.Models.Dto;
namespace CinemaCritic.Web.Services.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieGridDto>> GetMovies();
        Task<IEnumerable<TopMoviesDto>> GetTopMovies();
        Task<MovieDto> GetMovie(int id);
        Task<MovieDetailsDto> GetMovieDetails(int id);
    }
}

using CinemaCritic.Models.Dto;
namespace CinemaCritic.Web.Services.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieGridDto>> GetMovies();
        Task<MovieDto> GetMovie(int id);
        Task<MovieDetailsDto> GetMovieDetails(int id);
    }
}

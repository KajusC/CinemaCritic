using CinemaCritic.Models.Dto;
namespace CinemaCritic.Web.Services.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetMovies();
        Task<MovieDto> GetMovie(int id);
    }
}

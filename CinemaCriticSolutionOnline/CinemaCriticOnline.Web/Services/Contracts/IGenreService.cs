using CinemaCritic.Models.Dto;

namespace CinemaCritic.Web.Services.Contracts
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetGenres();
    }
}

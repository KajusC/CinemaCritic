using CinemaCritic.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaCritic.Web.Services.Contracts
{
    public interface IFavoriteMovieService
    {
        Task<ICollection<MovieGridDto>> GetFavoriteMoviesOfUser(int userId);
        Task<bool> AddFavoriteMovie(int userId, int movieId);
        Task<bool> DeleteFavoriteMovie(int userId, int movieId);
    }
}
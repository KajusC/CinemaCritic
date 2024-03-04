using CinemaCritic.API.Models;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IFavoriteMovieRepository
    {
        Task<ICollection<Movie>> GetAllFavoriteMoviesOfUser(int userId);
        Task<bool> CreateFavoriteMovie(int userId, int movieId);
        Task<bool> DeleteFavoriteMovie(int userId, int movieId);
        Task<bool> FavoriteMovieExists(int userId, int movieId);
        Task<bool> SaveChanges();
    }
}

using CinemaCritic.API.Models;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IFavoriteMovieRepository
    {
        ICollection<Movie> GetAllFavoriteMoviesOfUser(int userId);
        bool CreateFavoriteMovie(int userId, int movieId);
        bool DeleteFavoriteMovie(int userId, int movieId);
        bool FavoriteMovieExists(int userId, int movieId);
        bool SaveChanges();
    }
}

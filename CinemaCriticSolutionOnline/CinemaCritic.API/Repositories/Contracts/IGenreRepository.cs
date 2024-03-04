using CinemaCritic.API.Models;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IGenreRepository
    {
        Task<ICollection<Genre>> GetAllGenres();
        Task<Genre> GetGenre(int id);
        //if needed add more methods
        Task<bool> CreateGenre(Genre genre);
        Task<bool> UpdateGenre(Genre genre);
        Task<bool> DeleteGenre(Genre genre);
        Task<bool> GenreExists(int id);
        Task<bool> SaveChanges();
    }
}

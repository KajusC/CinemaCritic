using CinemaCritic.API.Models;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetAllGenres();
        Genre GetGenre(int id);
        //if needed add more methods
        bool CreateGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(Genre genre);
        bool GenreExists(int id);
        bool SaveChanges();
    }
}

using CinemaCritic.API.Data;
using CinemaCritic.API.Models;
using CinemaCritic.API.Repositories.Contracts;

namespace CinemaCritic.API.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;
        public GenreRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            return SaveChanges();
        }

        public bool DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            return SaveChanges();
        }

        public bool GenreExists(int id)
        {
            return _context.Genres.Any(g => g.Id == id);
        }

        public ICollection<Genre> GetAllGenres()
        {
            return _context.Genres.ToList();
        }

        public Genre GetGenre(int id)
        {
            return _context.Genres.Where(g => g.Id == id).FirstOrDefault();
        }

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateGenre(Genre genre)
        {
            _context.Genres.Update(genre);
            return SaveChanges();
        }
    }
}

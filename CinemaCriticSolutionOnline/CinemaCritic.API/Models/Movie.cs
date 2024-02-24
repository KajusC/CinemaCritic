using CinemaCritic.API.Models.JoinTables;

namespace CinemaCritic.API.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate {  get; set; }
        public Genre Genre { get; set; }
        public ICollection<Review> Reviews { get; set;}
        public ICollection<UserMovies> UserMovies { get; set; }
    }
}

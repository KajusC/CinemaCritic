using CinemaCritic.API.Models.JoinTables;

namespace CinemaCritic.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public ICollection<UserMovies> UserMovies { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}

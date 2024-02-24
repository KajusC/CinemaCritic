namespace CinemaCritic.API.Models.JoinTables
{
    public class UserMovies
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}

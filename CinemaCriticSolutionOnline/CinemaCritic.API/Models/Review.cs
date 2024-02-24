namespace CinemaCritic.API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public double Rating {  get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}

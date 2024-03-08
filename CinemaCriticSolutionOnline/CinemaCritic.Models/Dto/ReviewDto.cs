namespace CinemaCritic.Models.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaCritic.API.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public double Rating {  get; set; }

        public string Title { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Comment date is required")]
        public DateTime CommentDate { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}

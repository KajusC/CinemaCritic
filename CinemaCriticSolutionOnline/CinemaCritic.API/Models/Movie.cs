using CinemaCritic.API.Models.JoinTables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaCritic.API.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Movie name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Release date is required")]
        public DateTime ReleaseDate {  get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Trailer URL is required")]
        public string TrailerUrl { get; set; }

        [Required(ErrorMessage = "Movie length is required")]
        public string MovieLength { get; set; }

        [Required(ErrorMessage = "Age rating is required")]
        public string AgeRating { get; set; }

        public Genre Genre { get; set; }
        public ICollection<Review> Reviews { get; set;}
        public ICollection<UserMovies> UserMovies { get; set; }
    }
}

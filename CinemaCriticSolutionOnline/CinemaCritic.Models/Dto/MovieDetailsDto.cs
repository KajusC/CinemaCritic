using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaCritic.Models.Dto
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImageUrl { get; set; }
        public string TrailerUrl { get; set; }
        public string MovieLength { get; set; }
        public string AgeRating { get; set; }
        public GenreDto Genre { get; set; }
    }
}

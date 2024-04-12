using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaCritic.Models.Dto
{
    public class MovieGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImageUrl { get; set; }
        public string AgeRating { get; set; }
        public GenreDto Genre { get; set; }
    }
}

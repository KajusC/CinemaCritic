using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaCritic.Models.Dto
{
    public class TopMoviesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public string ImageUrl { get; set; }
    }
}

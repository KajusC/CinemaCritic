using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaCritic.Models.Dto
{
    public class ReviewListDto
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        public MovieDto Movie { get; set; } 
    }
}

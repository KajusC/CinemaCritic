using CinemaCritic.API.Models.JoinTables;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace CinemaCritic.API.Models
{
    public class User : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public ICollection<UserMovies> UserMovies { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}

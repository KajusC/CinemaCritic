using CinemaCritic.API.Models;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCritic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteMovieController : Controller
    {
        private readonly IFavoriteMovieRepository _favoriteMovieRepository;
        public FavoriteMovieController(IFavoriteMovieRepository favoriteMovieRepository)
        {
            _favoriteMovieRepository = favoriteMovieRepository;
        }

        [HttpGet("movies/{userId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Movie>))]
        public IActionResult GetFavoriteMoviesOfUser(int userId)
        {
            var favoriteMovies = _favoriteMovieRepository.GetAllFavoriteMoviesOfUser(userId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(favoriteMovies);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFavoriteMovie([FromQuery] int userId, [FromQuery] int movieId)
        {
            if(!_favoriteMovieRepository.CreateFavoriteMovie(userId, movieId))
            {
                ModelState.AddModelError("", $"Something went wrong while saving the record to the database");
                return StatusCode(500, ModelState);
            }
            return Ok("Success");
        }
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFavoriteMovie([FromQuery] int userId, [FromQuery] int movieId)
        {
            if(!_favoriteMovieRepository.DeleteFavoriteMovie(userId, movieId))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting the record from the database");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

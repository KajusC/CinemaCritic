using CinemaCritic.API.Repositories.Contracts;
using CinemaCritic.Models.Dto;
using Microsoft.AspNetCore.Mvc;



namespace CinemaCritic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteMovieController : ControllerBase
    {
        private readonly IFavoriteMovieRepository _favoriteMovieService;

        public FavoriteMovieController(IFavoriteMovieRepository favoriteMovieService)
        {
            _favoriteMovieService = favoriteMovieService;
        }

        [HttpGet("movies/{userId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<MovieGridDto>))]
        public async Task<IActionResult> GetFavoriteMoviesOfUser(int userId)
        {
            var favoriteMovies = await _favoriteMovieService.GetAllFavoriteMoviesOfUser(userId);
            return Ok(favoriteMovies);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddFavoriteMovie([FromQuery] int userId, [FromQuery] int movieId)
        {
            var success = await _favoriteMovieService.CreateFavoriteMovie(userId, movieId);
            if (!success)
            {
                ModelState.AddModelError("", $"Something went wrong while adding the favorite movie.");
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveFavoriteMovie([FromQuery] int userId, [FromQuery] int movieId)
        {
            var success = await _favoriteMovieService.DeleteFavoriteMovie(userId, movieId);
            if (!success)
            {
                ModelState.AddModelError("", $"Something went wrong while removing the favorite movie.");
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}

using AutoMapper;
using CinemaCritic.API.Dto;
using CinemaCritic.API.Models;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CinemaCritic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public MovieController(IMovieRepository movieRepository, IGenreRepository genreRepository, IMapper mapper)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(ICollection<Movie>))]
        public async Task<IActionResult> GetMovies()
        {
            var movies = _mapper.Map<List<MovieDto>>(await _movieRepository.GetAllMovies());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(movies);
        }
        [HttpGet("{movieId}")]
        [ProducesResponseType(200, Type=typeof(Movie))]
        public async Task<IActionResult> GetMovie(int movieId)
        {
            if(!await _movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }
            var movie = _mapper.Map<MovieDto>(await _movieRepository.GetMovie(movieId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(movie);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateMovie([FromQuery] int genreId, [FromBody] MovieDto movieToCreate)
        {
            if(movieToCreate is null)
            {
                return BadRequest(ModelState);
            }
            var movies = await _movieRepository.GetAllMovies();
            var movie = movies.Where(m => m.Name.Trim().ToUpper() == movieToCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (movie != null)
            {
                ModelState.AddModelError("", "Movie already exists");
                return StatusCode(422, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Movie movie1 = new Movie()
            {
                Name = movieToCreate.Name,
                Description = movieToCreate.Description,
                ReleaseDate = movieToCreate.ReleaseDate,
                ImageUrl = movieToCreate.ImageUrl
                
            };
            var genre = await _genreRepository.GetGenre(genreId);
            movie1.Genre = genre;
            if(!await _movieRepository.CreateMovie(movie1))
            {
                ModelState.AddModelError("", $"Something went wrong saving the movie {movie1.Name}");
                return StatusCode(500, ModelState);
            }
            return Ok("Success");
        }
        [HttpPut("{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateMovie(int movieId, [FromBody] MovieDto updatedMovie)
        {
            if(updatedMovie is null)
            {
                return BadRequest(ModelState);
            }
            if(movieId != updatedMovie.Id)
            {
                return BadRequest(ModelState);
            }
            if(!await _movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }
            var movie = _mapper.Map<Movie>(updatedMovie);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!await _movieRepository.UpdateMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong updating the movie {movie.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMovie(int movieId)
        {
            if(!await _movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }
            var movie = await _movieRepository.GetMovie(movieId);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!await _movieRepository.DeleteMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the movie {movie.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

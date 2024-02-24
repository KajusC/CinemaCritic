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
        public IActionResult GetMovies()
        {
            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetAllMovies());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(movies);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type=typeof(Movie))]
        public IActionResult GetMovie(int movieId)
        {
            if(!_movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }
            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovie(movieId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(movie);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovie([FromQuery] int genreId, [FromBody] MovieDto movieToCreate)
        {
            if(movieToCreate is null)
            {
                return BadRequest(ModelState);
            }
            var movie = _movieRepository.GetAllMovies()
               .Where(m => m.Name.Trim().ToUpper() == movieToCreate.Name.TrimEnd().ToUpper())
               .FirstOrDefault();
            if (movie != null)
            {
                ModelState.AddModelError("", "Movie already exists");
                return StatusCode(422, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            movie = _mapper.Map<Movie>(movieToCreate);
            var genre = _genreRepository.GetGenre(genreId);
            movie.Genre = genre;
            if(!_movieRepository.CreateMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong saving the movie {movie.Name}");
                return StatusCode(500, ModelState);
            }
            return Ok("Success");
        }
        [HttpPut("{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovie(int movieId, [FromBody] MovieDto updatedMovie)
        {
            if(updatedMovie is null)
            {
                return BadRequest(ModelState);
            }
            if(movieId != updatedMovie.Id)
            {
                return BadRequest(ModelState);
            }
            if(!_movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }
            var movie = _mapper.Map<Movie>(updatedMovie);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_movieRepository.UpdateMovie(movie))
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
        public IActionResult DeleteMovie(int movieId)
        {
            if(!_movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }
            var movie = _movieRepository.GetMovie(movieId);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_movieRepository.DeleteMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the movie {movie.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

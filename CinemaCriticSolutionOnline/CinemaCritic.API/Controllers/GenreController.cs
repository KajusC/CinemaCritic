using AutoMapper;
using CinemaCritic.API.Dto;
using CinemaCritic.API.Models;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCritic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Genre>))]
        public IActionResult GetGenres()
        {
            var genres = _mapper.Map<List<GenreDto>>(_genreRepository.GetAllGenres());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(genres);
        }
        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public IActionResult GetGenre(int genreId)
        {
            if(!_genreRepository.GenreExists(genreId))
            {
                return BadRequest("Genre does not exist");
            }
            var genre = _mapper.Map<GenreDto>(_genreRepository.GetGenre(genreId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(genre);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGenre([FromBody] GenreDto genreCreate)
        {
            if (genreCreate == null)
            {
                return BadRequest(ModelState);
            }
            var genre = _genreRepository.GetAllGenres()
                .Where(c => c.Name.Trim().ToUpper() == genreCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (genre != null)
            {
                ModelState.AddModelError("", "Genre already exists");
                return StatusCode(422, ModelState);
            }
            _genreRepository.CreateGenre(_mapper.Map<Genre>(genreCreate));
            if (!_genreRepository.SaveChanges())
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Success");
        }
        [HttpPut("{genreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGenre(int genreId, [FromBody] GenreDto genreToUpdate)
        {
            if(genreToUpdate is null)
            {
                return BadRequest(ModelState);
            }
            if(genreToUpdate.Id != genreId)
            {
                return BadRequest(ModelState);
            }
            if (!_genreRepository.GenreExists(genreId))
            {
                return NotFound("Genre does not exist");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genreMap = _mapper.Map<Genre>(genreToUpdate);
            if (!_genreRepository.UpdateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{genreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
            {
                return NotFound("Genre does not exist");
            }
            var genre = _genreRepository.GetGenre(genreId);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_genreRepository.DeleteGenre(genre))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}

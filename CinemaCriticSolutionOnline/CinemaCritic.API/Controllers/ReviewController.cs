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
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IUserRepository userRepository, IMovieRepository movieRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetAllReviews());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if(!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateReview([FromBody] ReviewDto reviewCreate, [FromQuery] int userId, [FromQuery] int movieId)
        {
            if(reviewCreate is null)
            {
                return BadRequest(ModelState);
            }
            var review = _mapper.Map<Review>(reviewCreate);
            if(!_userRepository.UserExists(userId))
            {
                return NotFound("User does not exist");
            }
            if(!_movieRepository.MovieExists(movieId))
            {
                return NotFound("Movie does not exist");
            }
            review.User = _userRepository.GetUser(userId);
            review.Movie = _movieRepository.GetMovie(movieId);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_reviewRepository.CreateReview(review))
            {
                ModelState.AddModelError("", $"Something went wrong saving the review");
                return StatusCode(500, ModelState);
            }
            return Ok("Success");

        }
        [HttpPut("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto reviewUpdate)
        {
            if(reviewUpdate is null)
            {
                return BadRequest(ModelState);
            }
            if(reviewId != reviewUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if(!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = _mapper.Map<Review>(reviewUpdate);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_reviewRepository.UpdateReview(review))
            {
                ModelState.AddModelError("", $"Something went wrong updating the review");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if(!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = _reviewRepository.GetReview(reviewId);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_reviewRepository.DeleteReview(review))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the review");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

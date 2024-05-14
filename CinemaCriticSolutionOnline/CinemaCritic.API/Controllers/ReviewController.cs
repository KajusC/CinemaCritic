using AutoMapper;
using CinemaCritic.API.Models;
using CinemaCritic.API.Repositories.Contracts;
using CinemaCritic.Models.Dto;
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
        public async Task<IActionResult> GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(await _reviewRepository.GetAllReviews());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReview(int reviewId)
        {
            if(!await _reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = _mapper.Map<ReviewDto>(await _reviewRepository.GetReview(reviewId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        public async Task<IActionResult> GetReviewsOfUser(int userId)
        {
            var reviews = _mapper.Map<List<ReviewListDto>>(await _reviewRepository.GetReviewsOfUser(userId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateReview([FromBody] ReviewDto reviewCreate, [FromQuery] int userId, [FromQuery] int movieId)
        {
            if(reviewCreate is null)
            {
                return BadRequest(ModelState);
            }
            Review review = new Review()
            {
                Rating = reviewCreate.Rating,
                Title = reviewCreate.Title,
                Comment = reviewCreate.Comment,
                CommentDate = reviewCreate.CommentDate
            };
            if(!await _userRepository.UserExists(userId))
            {
                return NotFound("User does not exist");
            }
            if(!await _movieRepository.MovieExists(movieId))
            {
                return NotFound("Movie does not exist");
            }
            review.User = await _userRepository.GetUser(userId);
            review.Movie = await _movieRepository.GetMovie(movieId);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!await _reviewRepository.CreateReview(review))
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
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewDto reviewUpdate)
        {
            if(reviewUpdate is null)
            {
                return BadRequest(ModelState);
            }
            if(reviewId != reviewUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if(!await _reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = _mapper.Map<Review>(reviewUpdate);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!await _reviewRepository.UpdateReview(review))
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
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            if(!await _reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = await _reviewRepository.GetReview(reviewId);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!await _reviewRepository.DeleteReview(review))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the review");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpGet("movie/{movieId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        public async Task<IActionResult> GetReviewsOfMovie(int movieId)
        {
            var reviews = _mapper.Map<List<ReviewOfMovieDto>>(await _reviewRepository.GetReviewsOfMovie(movieId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpGet("movie/review/{movieId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        public async Task<IActionResult> GetReviewsOfMovies(int movieId)
        {
            var reviews = _mapper.Map<List<ReviewListDto>>(await _reviewRepository.GetReviewsOfMovieWithMovie(movieId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
    }
}

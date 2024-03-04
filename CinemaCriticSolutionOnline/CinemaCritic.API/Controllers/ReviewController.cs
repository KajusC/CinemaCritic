﻿using AutoMapper;
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
                Comment = reviewCreate.Comment
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
    }
}

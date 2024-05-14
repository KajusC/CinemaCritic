using CinemaCritic.API.Models;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IReviewRepository
    {
        Task<ICollection<Review>> GetAllReviews();
        Task<Review> GetReview(int id);
        Task<ICollection<Review>> GetReviewsOfUser(int userId);
        Task<ICollection<Review>> GetReviewsOfMovie(int movieId);
        Task<ICollection<Review>> GetReviewsOfMovieWithMovie(int movieId);
        Task<bool> CreateReview(Review review);
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(Review review);
        Task<bool> ReviewExists(int id);
        Task<bool> SaveChanges();
    }
}

using CinemaCritic.API.Models;

namespace CinemaCritic.API.Repositories.Contracts
{
    public interface IReviewRepository
    {
        Task<ICollection<Review>> GetAllReviews();
        Task<Review> GetReview(int id);
        //if needed add more methods
        Task<bool> CreateReview(Review review);
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(Review review);
        Task<bool> ReviewExists(int id);
        Task<bool> SaveChanges();
    }
}

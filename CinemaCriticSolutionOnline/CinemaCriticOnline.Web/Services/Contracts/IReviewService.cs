using CinemaCritic.Models.Dto;

namespace CinemaCritic.Web.Services.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewListDto>> GetReviewsOfUser(int userId);
		Task UpdateReview(int id, ReviewDto review);
        Task<ReviewDto> GetReview(int reviewId);
		Task DeleteReview(int reviewId);
    }
}

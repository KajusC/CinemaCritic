using CinemaCritic.Models.Dto;

namespace CinemaCritic.Web.Services.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewListDto>> GetReviewsOfUser(int userId);
        Task DeleteReview(int reviewId);
    }
}

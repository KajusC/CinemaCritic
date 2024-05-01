using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Net.Http.Json;
using System.Text;

namespace CinemaCritic.Web.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;
        public ReviewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ReviewListDto>> GetReviewsOfUser(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Review/User/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ReviewListDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ReviewListDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode}: {message}");
                }
            }
            catch (Exception)
            {
                //log error
                throw;
            }
        }

        public async Task UpdateReview(int id, ReviewDto review)
        {
	        try
	        {
		        var response = await _httpClient.PutAsJsonAsync($"api/Review/{id}", review);

		        if (!response.IsSuccessStatusCode)
		        {
			        var message = await response.Content.ReadAsStringAsync();
			        throw new Exception($"{response.StatusCode}: {message}");
		        }
	        }
	        catch (Exception)
	        {
		        throw new Exception("An error occurred while updating the review.");
			}
		}

        public async Task<ReviewDto> GetReview(int reviewId)
        {
	        try
	        {
		        var response = await _httpClient.GetAsync($"api/Review/{reviewId}");

		        if (response.IsSuccessStatusCode)
		        {
			        return await response.Content.ReadFromJsonAsync<ReviewDto>();
		        }
		        else
		        {
			        var message = await response.Content.ReadAsStringAsync();
			        throw new Exception($"{response.StatusCode}: {message}");
		        }
	        }
	        catch (Exception)
	        {
				throw new Exception("An error occurred while retrieving the review.");
				throw;
			}
		}

        public async Task DeleteReview(int reviewId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Review/{reviewId}");

                if (!response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode}: {message}");
                }
            }
            catch (Exception)
            {
                // Log error
                throw;
            }
        }
        public async Task AddReview(ReviewDto review, int movieId, int userId)
        {
            try
            {
                var jsoned = JsonConvert.SerializeObject(review);
                var content = new StringContent(jsoned, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/Review?userId={userId}&movieId={movieId}", content);

                if (!response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode}: {message}");
                }
            }
            catch (Exception)
            {
                // Log error
                throw;
            }
        }
    }
}

using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using System.Net.Http.Json;

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
    }
}

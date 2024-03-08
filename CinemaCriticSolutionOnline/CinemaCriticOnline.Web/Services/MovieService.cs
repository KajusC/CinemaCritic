using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using System.Net.Http.Json;

namespace CinemaCritic.Web.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Movie");

                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<MovieDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>();
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

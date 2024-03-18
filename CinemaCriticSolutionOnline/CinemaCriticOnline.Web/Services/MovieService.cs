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

        public async Task<MovieDto> GetMovie(int movieId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Movie/{movieId}");

                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MovieDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode}: {message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<MovieDetailsDto> GetMovieDetails(int movieId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Movie/details/{movieId}");
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MovieDetailsDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode}: {message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
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

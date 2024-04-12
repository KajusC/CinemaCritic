using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using System.Net.Http.Json;

namespace CinemaCritic.Web.Services
{
    public class GenreService : IGenreService
    {
        private readonly HttpClient _httpClient;
        public GenreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<GenreDto>> GetGenres()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Genre");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<GenreDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<GenreDto>>();
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

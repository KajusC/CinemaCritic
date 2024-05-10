using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace CinemaCritic.Web.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;


        public MovieService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            this.localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<MovieDto> GetMovie(int movieId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Movie/{movieId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MovieDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode}: {message}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get movie", ex);
            }
        }
        public async Task<MovieDetailsDto> GetMovieDetails(int movieId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Movie/details/{movieId}");
                if (response.IsSuccessStatusCode)
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
        public async Task<IEnumerable<TopMoviesDto>> GetTopMovies()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Movie");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<TopMoviesDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<TopMoviesDto>>();
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
        public async Task<IEnumerable<MovieGridDto>> GetMovies()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Movie");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<MovieGridDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<MovieGridDto>>();
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

        private async Task<string> GetJwtToken()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            await Console.Out.WriteLineAsync(user.ToString());
            try
            {
                return await localStorageService.GetItem<string>("token");
            }
            catch
            {
                throw new InvalidOperationException("User is not authenticated.");
            }
        }


    }
}

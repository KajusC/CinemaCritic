using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CinemaCritic.Web.Services
{
    public class FavoriteMovieService : IFavoriteMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public FavoriteMovieService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ICollection<MovieGridDto>> GetFavoriteMoviesOfUser(int userId)
        {
            try
            {
                var token = await GetJwtToken();

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"api/FavoriteMovie/movies/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ICollection<MovieGridDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode}: {message}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get favorite movies of user", ex);
            }
        }

        public async Task<bool> AddFavoriteMovie(int userId, int movieId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/FavoriteMovie?userId={userId}&movieId={movieId}", null);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create favorite movie", ex);
            }
        }

        public async Task<bool> DeleteFavoriteMovie(int userId, int movieId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/FavoriteMovie?userId={userId}&movieId={movieId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete favorite movie", ex);
            }
        }

        private async Task<string> GetJwtToken()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            try
            {
                return await _localStorageService.GetItem<string>("token");
            }
            catch
            {
                throw new InvalidOperationException("User is not authenticated.");
            }
        }
    }
}
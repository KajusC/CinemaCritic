using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace CinemaCritic.Web.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;


        public UserService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            this.localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<UserDto> GetUser(int userID)
        {
            try
            {
                var token = await GetJwtToken();

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"api/User/{userID}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode}: {message}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get user", ex);
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
using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

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
        public async Task<bool> UpdateUser(int userId, UserDto model)
        {
            try
            {
                var newUser = new UserDto
                {
                    Id = model.Id,
                    Username = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                };

                await Console.Out.WriteLineAsync(newUser.Email);
                var jsoned = JsonConvert.SerializeObject(newUser);
                var content = new StringContent(jsoned, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/User/{userId}", content);

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
                throw new FatalRegisterException(ex.ToString()); // Re-throw the exception for handling it in the caller method
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
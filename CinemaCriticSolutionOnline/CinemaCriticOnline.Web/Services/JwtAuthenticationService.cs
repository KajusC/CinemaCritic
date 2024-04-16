using CinemaCritic.Web.Models;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace CinemaCritic.Web.Services
{
    public class JwtAuthenticationService : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private ILocalStorageService _localStorageService;

        private static AuthenticationState NotAuthenticatedState = new AuthenticationState(new System.Security.Claims.ClaimsPrincipal());

        private LoginUser _user;

        public JwtAuthenticationService(HttpClient client, ILocalStorageService localStorageService)
        {
            _httpClient = client;
            _localStorageService = localStorageService;
            Initialize();
        }

        public string DisplayName => this._user?.DisplayName;

        public bool IsLoggedIn => this._user != null;
        public bool logged = false;

        public string Token => this._user?.Jwt;

        private async Task Initialize()
        {
            _user = await _localStorageService.GetItem<LoginUser>("user");
        }
        public async void Login(string jwt)
        {
            var principal = JwtSerialize.Deserialize(jwt);
            this._user = new LoginUser(principal.Identity.Name, jwt, principal);
            // Store token in localStorage upon login
            await _localStorageService.SetItem("token", jwt);
            await _localStorageService.SetItem("user", _user);
            this.NotifyAuthenticationStateChanged(Task.FromResult(GetState()));
            await Task.Delay(1);
            logged = true;
        }

        public async Task<bool> CheckIfUserIsInDatabase(LoginModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/user/{model.Username}", model);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Login failed.", ex);
            }
        }

        public async Task<string> GetJwtTokenFromLogin(LoginModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/WebApiAuthentication/Login", model);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    return loginResponse?.JwtToken;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Login failed.", ex);
            }
        }
        public async Task Logout()
        {
            this._user = null;
            await _localStorageService.RemoveItem("token");
            await _localStorageService.RemoveItem("user");
            this.NotifyAuthenticationStateChanged(Task.FromResult(GetState()));
            logged = false;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = _localStorageService.GetItem<string>("token");
                if (token != null)
                {
                    return Task.FromResult(GetState());
                }

            }
            catch (Exception)
            {
                throw new Exception("User is biiim bam.");
            }
            return Task.FromResult(NotAuthenticatedState);
        }
        private AuthenticationState GetState()
        {
            if (this._user != null)
            {
                return new AuthenticationState(this._user.ClaimsPrincipal);
            }
            else
            {
                return NotAuthenticatedState;
            }
        }

    }
}

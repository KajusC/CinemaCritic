using CinemaCritic.Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Http.Json;
using System.Reflection;
using Blazored.SessionStorage;
using CinemaCritic.Web.Services.Contracts;
using System.Net.Http;
using Microsoft.JSInterop;

namespace CinemaCritic.Web
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string TokenKey = "token";
        private readonly IJSRuntime _jsRuntime;

        public AuthenticationService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public event Action<string?>? LoginChange;

        public ValueTask<string> GetJwtAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            string token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }
        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }

        public async Task<DateTime> LoginAsync(string token)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
            return DateTime.Now;

        }
        public Task<bool> RefreshAsync()
        {
            throw new NotImplementedException();
        }
    }
}

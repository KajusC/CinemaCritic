using CinemaCritic.Web.Models;

namespace CinemaCritic.Web.Services.Contracts
{
    public interface IAuthenticationService
    {
        event Action<string?>? LoginChange;

        ValueTask<string> GetJwtAsync();
        Task<DateTime> LoginAsync(string token);
        Task LogoutAsync();
        Task<bool> RefreshAsync();
        Task<bool> IsAuthenticatedAsync();
    }
}

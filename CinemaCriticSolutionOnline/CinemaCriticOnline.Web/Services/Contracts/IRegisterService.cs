using CinemaCritic.Web.Models;

namespace CinemaCritic.Web.Services.Contracts
{
    public interface IRegisterService
    {
        Task RegisterAsync(RegisterModel model);

    }
}

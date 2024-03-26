using System.Security.Claims;

namespace CinemaCritic.Web.Models
{
    public class LoginUser
    {
        public string DisplayName { get; }
        public string Jwt { get; }
        public ClaimsPrincipal ClaimsPrincipal { get; }

        public LoginUser(string displayName, string jwt, ClaimsPrincipal claimsPrincipal)
        {
            DisplayName = displayName;
            Jwt = jwt;
            ClaimsPrincipal = claimsPrincipal;
        }
    }

}

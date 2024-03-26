using Newtonsoft.Json.Linq;

namespace CinemaCritic.API.Authentication
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string JwtToken { get; set; }

        public LoginResponse(bool success, string jwtToken)
        {
            Success = success;
            JwtToken = jwtToken;
        }

        public static LoginResponse Failed => new LoginResponse(false, null);
    }
}

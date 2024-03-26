namespace CinemaCritic.Web.Models
{
    public class LoginResponse
    {
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
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

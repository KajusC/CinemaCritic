namespace CinemaCritic.API.Authentication
{
    public class LoginModel
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}

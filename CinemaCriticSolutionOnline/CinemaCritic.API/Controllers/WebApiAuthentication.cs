using CinemaCritic.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CinemaCritic.API.Authentication;
using CinemaCritic.Models.Dto;

namespace CinemaCritic.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WebApiAuthentication : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;

        public WebApiAuthentication(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody]RegisterModel request)
        {
            User? existingUser = null;
            try
            {
                existingUser = await _userManager.FindByNameAsync(request.UserName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Failed to find user: {ex.Message}");
            }
            

            if (existingUser != null)
                return Conflict("User already exists.");

            var user = new User()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var hashedPassword = _passwordHasher.HashPassword(user, request.Password);
                user.PasswordHash = hashedPassword;


            var result = await this._userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return Ok("User successfully created");
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError,
                       $"Failed to create user: {string.Join(" ", result.Errors.Select(e => e.Description))}");
        }

        [HttpPost("[action]")]
        public async Task<LoginResponse> Login([FromBody] LoginModel request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                // Verify the hashed password
                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Success)
                {
                    string jwt = CreateJWT(user);
                    AppendRefreshTokenCookie(user, HttpContext.Response.Cookies);

                    return new LoginResponse(true, jwt);
                }
                return new LoginResponse(true, "Random");
            }

            return LoginResponse.Failed;
        }

        private static void AppendRefreshTokenCookie(User user, IResponseCookies cookies)
        {
            var options = new CookieOptions();
            options.HttpOnly = true;
            options.Secure = true;
            options.SameSite = SameSiteMode.Strict;
            options.Expires = DateTime.Now.AddMinutes(60);
            cookies.Append("fr0MwzuyRdoodkyl9GQBjFsehxdyfjfAApgWGMkbxxn5Bqu69xB98qMpKQdkPWR", user.SecurityStamp, options);
        }

        private static string CreateJWT(User user)
        {
            
            var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("fr0MwzuyRdoodkyl9GQBjFsehxdyfjfAApgWGMkbxxn5Bqu69xB98qMpKQdkPWR"));
            var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName), // NOTE: this will be the "User.Identity.Name" value
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5278",
                audience: "http://localhost:5278",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("[action]")]
        public LoginResponse RefreshToken()
        {
            var cookie = HttpContext.Request.Cookies["fr0MwzuyRdoodkyl9GQBjFsehxdyfjfAApgWGMkbxxn5Bqu69xB98qMpKQdkPWR"];
            Console.WriteLine("Te");
            if (cookie != null)
            {
                var user = _userManager.Users.FirstOrDefault(user => user.SecurityStamp == cookie);
                Console.WriteLine("AAAAAAAAAAAAAT");
                if (user != null)
                {
                    var jwtToken = CreateJWT(user);
                    return new LoginResponse(true, jwtToken);
                }
            }

            return LoginResponse.Failed;
        }
    }
}
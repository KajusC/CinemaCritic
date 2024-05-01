using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace CinemaCritic.Web.Pages
{
    public class ReviewBase : ComponentBase
    {
        [Inject]
        public IReviewService _reviewService { get; set; }

        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public ILocalStorageService _localStorageService { get; set; }
        [Parameter]
        public int Id { get; set; }

        public async Task AddReview(ReviewDto review, int movieId)
        {
            try
            {
                var logged = await _authService.IsAuthenticatedAsync();
                
                if (logged)
                {
                    int userId;
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(await _localStorageService.GetItem<string>("token"));
                    userId = int.Parse(token.Claims.First(claim => claim.Type == "jti").Value);
                    await _reviewService.AddReview(review, movieId, userId);

                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}

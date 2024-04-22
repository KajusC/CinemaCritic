using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.IdentityModel.Tokens.Jwt;

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
        public IEnumerable<ReviewListDto> Reviews { get; set; } = new List<ReviewListDto>();

        public string ErrorMessage { get; set; }

        override protected async Task OnInitializedAsync()
        {
            try
            {
                var logged = await _authService.IsAuthenticatedAsync();
                if (logged)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(await _localStorageService.GetItem<string>("token"));
                    var userId = token.Claims.First(claim => claim.Type == "jti").Value;

                    Reviews = await _reviewService.GetReviewsOfUser(int.Parse(userId));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }   
    }
}

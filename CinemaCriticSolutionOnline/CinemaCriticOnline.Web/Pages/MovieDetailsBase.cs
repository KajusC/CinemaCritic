using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace CinemaCritic.Web.Pages
{
    public class MovieDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IMovieService MovieService { get; set; }

        public MovieDetailsDto MovieDetailsDto { get; set; }
        public string ErrorMessage { get; set; }
        override protected async Task OnInitializedAsync()
        {
            try
            {
                MovieDetailsDto = await MovieService.GetMovieDetails(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}

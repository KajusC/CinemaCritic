using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace CinemaCritic.Web.Pages
{
    public class MovieDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        public double AverageRating { get; set; }

        [Inject]
        public IMovieService MovieService { get; set; }

        [Inject]
        public IReviewService ReviewService { get; set; }

        public MovieDetailsDto MovieDetailsDto { get; set; }
        public string ErrorMessage { get; set; }
        override protected async Task OnInitializedAsync()
        {
            try
            {
                double average;
                MovieDetailsDto = await MovieService.GetMovieDetails(Id);
                average = await ReviewService.GetMovieAverage(Id);
                AverageRating = (double)MathF.Round((float)average,2);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}

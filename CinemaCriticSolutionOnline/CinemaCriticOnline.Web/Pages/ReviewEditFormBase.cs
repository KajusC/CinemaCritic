using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace CinemaCritic.Web.Pages
{
	public class ReviewEditFormBase : ComponentBase
	{
		public ReviewDto ReviewEditDto { get; set; } = new ReviewDto();
		public string ErrorMessage { get; set; }
		[Inject]
		public IReviewService reviewService { get; set; }
		[Inject]
		public NavigationManager navigationManager { get; set; }
		[Parameter]
		public int ReviewId { get; set; }

        override protected async Task OnInitializedAsync()
        {
            try
            {
	            ReviewEditDto = await reviewService.GetReview(ReviewId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
	}
}

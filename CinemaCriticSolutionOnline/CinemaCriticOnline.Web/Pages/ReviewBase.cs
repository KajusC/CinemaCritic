using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace CinemaCritic.Web.Pages
{
	public class ReviewBase : ComponentBase
	{
		[Parameter]
        public int Id { get; set; }

        [Inject]
        public IReviewService MovieService { get; set; }

        public string ErrorMessage { get; set; }
    }
}

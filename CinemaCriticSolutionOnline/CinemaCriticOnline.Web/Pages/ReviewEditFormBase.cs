using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;

namespace CinemaCritic.Web.Pages
{
    public class ReviewEditFormBase : ComponentBase
    {
        public ReviewDto ReviewEditDto { get; set; } = new ReviewDto();
        public string ErrorMessage { get; set; }
        [Inject]
        public IReviewService ReviewService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public int ReviewId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ReviewEditDto = await ReviewService.GetReview(ReviewId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected Task UpdateReview(ReviewDto reviewDto)
        {
            try
            {
                ReviewService.UpdateReview(ReviewId, reviewDto);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return Task.CompletedTask;
        }
    }
}
using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace CinemaCritic.Web.Pages
{
    public class MoviesBase : ComponentBase
    {
        [Inject]
        public IMovieService _movieService { get; set; }
        [Parameter]
        public IEnumerable<MovieDto> Movies { get; set; } 

        override protected async Task OnInitializedAsync()
        {
            Movies = await _movieService.GetMovies();
        }
    }
}

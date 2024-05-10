using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace CinemaCritic.Web.Pages
{
    public class TopMoviesBase : ComponentBase
    {
        [Inject]
        public IMovieService _movieService { get; set; }

        [Inject]
        public IGenreService _genreService { get; set; }

        [Parameter]
        public IEnumerable<TopMoviesDto> Movies { get; set; }

        [Inject]
        IAuthenticationService _authService { get; set; }

        override protected async Task OnInitializedAsync()
        {
            try
            {
                var logged = await _authService.IsAuthenticatedAsync();
                if (logged)
                {
                    Movies = await _movieService.GetTopMovies();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

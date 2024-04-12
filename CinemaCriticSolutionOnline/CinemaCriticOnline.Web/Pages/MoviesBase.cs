using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace CinemaCritic.Web.Pages
{
    public class MoviesBase : ComponentBase
    {
        [Inject]
        public IMovieService _movieService { get; set; }

        [Inject]
        public IGenreService _genreService { get; set; }

        [Parameter]
        public IEnumerable<MovieGridDto> Movies { get; set; }

        [Parameter]
        public IEnumerable<GenreDto> Genres { get; set;}

        override protected async Task OnInitializedAsync()
        {
            Movies = await _movieService.GetMovies();
            Genres = await _genreService.GetGenres();
        }
    }
}

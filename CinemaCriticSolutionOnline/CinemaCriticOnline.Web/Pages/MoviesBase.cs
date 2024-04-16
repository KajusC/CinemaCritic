﻿using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services;
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

        [Inject]
        IAuthenticationService _authService { get; set; }

        [Parameter]
        public IEnumerable<GenreDto> Genres { get; set;}
        public IEnumerable<MovieDto> Movies { get; set; }

        override protected async Task OnInitializedAsync()
        {
            Movies = await _movieService.GetMovies();
            Genres = await _genreService.GetGenres();
        }
    }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var logged = await _authService.IsAuthenticatedAsync();
                if (logged)
                {
                    Movies = await _movieService.GetMovies();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

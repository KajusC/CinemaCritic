using CinemaCritic.Models.Dto;
using CinemaCritic.Web.Services;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace CinemaCritic.Web.Pages
{
    public class MoviesBase : ComponentBase
    {
        [Inject]
        public IMovieService _movieService { get; set; }

        [Inject]
        public IGenreService _genreService { get; set; }

        [Inject]
        public ILocalStorageService _localStorageService { get; set; }

        [Parameter]
        public IEnumerable<MovieGridDto> Movies { get; set; }

        [Parameter]
        public IEnumerable<GenreDto> Genres { get; set; }

        public Dictionary<int, bool> favoriteStatus = new Dictionary<int, bool>();

        [Inject]
        protected IAuthenticationService _authService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var logged = await _authService.IsAuthenticatedAsync();
                if (logged)
                {
                    Movies = await _movieService.GetMovies();
                    Genres = await _genreService.GetGenres();

                    // Load favorite status from local storage
                    var storedFavoriteStatus = await _localStorageService.GetItem<Dictionary<int, bool>>("favoriteStatus");
                    if (storedFavoriteStatus != null)
                    {
                        favoriteStatus = storedFavoriteStatus;
                    }
                    else
                    {
                        // If no favorite status found in local storage, initialize with default values
                        foreach (var movie in Movies)
                        {
                            favoriteStatus[movie.Id] = false;
                        }
                    }

                    // Fetch favorite status of movies
                   // await FetchFavoriteStatus();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Method to fetch favorite status of movies
        /*private async Task FetchFavoriteStatus()
        {

            try
            {
                var logged = await _authService.IsAuthenticatedAsync();
                if (logged)
                {
                    int userId;
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(await _localStorageService.GetItem<string>("token"));
                    userId = int.Parse(token.Claims.First(claim => claim.Type == "jti").Value);

                    // Fetch favorite movies of the user
                    var favoriteMovies = await _favoriteMovieService.GetFavoriteMoviesOfUser(userId);

                    // Update favorite status dictionary
                    foreach (var movie in Movies)
                    {
                        favoriteStatus[movie.Id] = favoriteMovies.Contains(movie);
                    }

                    // Update UI
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching favorite status: {ex.Message}");
            }
        }*/

       /* // Method to handle favorite movie click event
        protected async Task ToggleFavorite(int movieId)
        {
            try
            {
                var logged = await _authService.IsAuthenticatedAsync();

                if (logged)
                {
                    int userId;
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(await _localStorageService.GetItem<string>("token"));
                    userId = int.Parse(token.Claims.First(claim => claim.Type == "jti").Value);

                    if (favoriteStatus.ContainsKey(movieId) && favoriteStatus[movieId])
                    {
                        // Remove from favorites
                        await _favoriteMovieService.DeleteFavoriteMovie(userId, movieId);
                        favoriteStatus[movieId] = false;
                    }
                    else
                    {
                        // Add to favorites
                        await _favoriteMovieService.AddFavoriteMovie(userId, movieId);
                        favoriteStatus[movieId] = true;
                    }

                    // Update local storage
                    await _localStorageService.SetItem("favoriteStatus", favoriteStatus);

                    // Update UI
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while toggling favorite: {ex.Message}");
            }

        }*/
    }
}
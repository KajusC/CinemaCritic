using CinemaCritic.Models.Dto;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using CinemaCritic.Web.Services.Contracts;
namespace CinemaCritic.Web.Pages
{
    public class DisplayMoviesBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<MovieGridDto> Movies { get; set; }

        [Inject]
        public ILocalStorageService _localStorageService { get; set; }

        [Inject]
        protected IAuthenticationService _authService { get; set; }
    }
}
using CinemaCritic.Models.Dto;
using Microsoft.AspNetCore.Components;

namespace CinemaCritic.Web.Pages
{
    public class DisplayMoviesBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<MovieGridDto> Movies { get; set; }
    }
}

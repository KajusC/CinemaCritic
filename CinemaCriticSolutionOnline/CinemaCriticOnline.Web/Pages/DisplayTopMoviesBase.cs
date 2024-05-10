using CinemaCritic.Models.Dto;
using Microsoft.AspNetCore.Components;

namespace CinemaCritic.Web.Pages
{
    public class DisplayTopMoviesBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<TopMoviesDto> Movies { get; set; }
    }
}

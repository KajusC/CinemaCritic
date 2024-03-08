using CinemaCritic.Web;
using CinemaCritic.Web.Services;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5223/") });
builder.Services.AddScoped<IMovieService, MovieService>();

await builder.Build().RunAsync();
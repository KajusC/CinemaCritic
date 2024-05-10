using CinemaCritic.Web;
using CinemaCritic.Web.Services;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using CinemaCritic.Web.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using CinemaCritic.Web.Pages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var appUri = new Uri(builder.HostEnvironment.BaseAddress);

builder.Services.AddScoped(provider => new JwtTokenMessageHandler(appUri, provider.GetRequiredService<JwtAuthenticationService>()));
builder.Services.AddHttpClient("MyApp.ServerAPI", client => client.BaseAddress = appUri)
    .AddHttpMessageHandler<JwtTokenMessageHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5223/") });
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IFavoriteMovieService, FavoriteMovieService>();
builder.Services.AddScoped<JwtAuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<JwtAuthenticationService>());
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<UserService>();

builder.Services.AddHttpClient();

await builder.Build().RunAsync();

await RefreshJwtToken(builder.Build());


static async Task RefreshJwtToken(WebAssemblyHost application)
{
    using var bootstrapScope = application.Services.CreateScope();
    var httpClient = bootstrapScope.ServiceProvider.GetRequiredService<HttpClient>();

    try
    {
        var response = await httpClient.PostAsync("api/WebApiAuthentication/RefreshToken", null);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();

        }
        else
        {
            throw new Exception("Refresh token request failed.");
        }
    }
    catch (Exception ex)
    {
        // Handle exception
    }
}


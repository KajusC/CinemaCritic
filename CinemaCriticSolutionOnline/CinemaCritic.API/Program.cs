using CinemaCritic.API.Data;
using CinemaCritic.API.Repositories;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.Json.Serialization;
using Microsoft.Extensions.ObjectPool;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IFavoriteMovieRepository, FavoriteMovieRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

builder.Services.AddDbContext<DataContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
policy.WithOrigins("https://localhost:5223", "http://localhost:5278")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

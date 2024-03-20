using CinemaCritic.API.Data;
using CinemaCritic.API.Repositories;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.Json.Serialization;
using Microsoft.Extensions.ObjectPool;
using CinemaCritic.API.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using CinemaCritic.API.Authentication;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

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

builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter 'Bearer [jwt]'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme, Array.Empty<string>() } });
});

using var loggerFactory = LoggerFactory.Create(b => b.SetMinimumLevel(LogLevel.Trace).AddConsole());

var secret = builder.Configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
        ClockSkew = new TimeSpan(0, 0, 5)
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = ctx => LogAttempt(ctx.Request.Headers, "OnChallenge"),
        OnTokenValidated = ctx => LogAttempt(ctx.Request.Headers, "OnTokenValidated")
    };
});

const string policy = "defaultPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(policy,
                      p =>
                      {
                          p.AllowAnyHeader();
                          p.AllowAnyMethod();
                          p.AllowAnyHeader();
                          p.AllowAnyOrigin();
                      });
});

var app = builder.Build();

//PopulateDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy =>
policy.WithOrigins("https://localhost:5223", "http://localhost:5278")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType));


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

Task LogAttempt(IHeaderDictionary headers, string eventType)
{
    var logger = loggerFactory.CreateLogger<Program>();

    var authorizationHeader = headers["Authorization"].FirstOrDefault();

    if (authorizationHeader is null)
        logger.LogInformation($"{eventType}. JWT not present");
    else
    {
        string jwtString = authorizationHeader.Substring("Bearer ".Length);

        var jwt = new JwtSecurityToken(jwtString);

        logger.LogInformation($"{eventType}. Expiration: {jwt.ValidTo.ToLongTimeString()}. System time: {DateTime.UtcNow.ToLongTimeString()}");
    }

    return Task.CompletedTask;
}

//void PopulateDb()
//{
//    using var scope = app.Services.CreateScope();

//    using var db = scope.ServiceProvider.GetRequiredService<ReviewContext>();

//    db.BookReviews.Add(new() { Title = "Dr No", Rating = 4 });
//    db.BookReviews.Add(new() { Title = "Goldfinger", Rating = 3 });
//    db.BookReviews.Add(new() { Title = "From Russia with Love", Rating = 1 });
//    db.BookReviews.Add(new() { Title = "Moonraker", Rating = 4 });
//    db.BookReviews.Add(new() { Title = "Dr No", Rating = 5 });
//    db.BookReviews.Add(new() { Title = "Moonraker", Rating = 2 });
//    db.BookReviews.Add(new() { Title = "Dr No", Rating = 2});
//    db.BookReviews.Add(new() { Title = "From Russia with Love", Rating = 5 });
//    db.BookReviews.Add(new() { Title = "From Russia with Love", Rating = 3 });

//    db.SaveChanges();
//}

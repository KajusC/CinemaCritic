using AutoMapper;
using CinemaCritic.API.Dto;
using CinemaCritic.API.Models;

namespace CinemaCritic.API.Helper
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
        }
    }
}

using AutoMapper;
using CinemaCritic.API.Models;
using CinemaCritic.API.Models.JoinTables;
using CinemaCritic.Models.Dto;

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
            CreateMap<UserMovies, FavoriteMovieDto>();
            CreateMap<FavoriteMovieDto, UserMovies>();
            CreateMap<Movie, MovieDetailsDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre));
            CreateMap<Movie, MovieGridDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre));
            CreateMap<MovieDetailsDto, Movie>();
        }
    }
}

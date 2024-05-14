using AutoMapper;
using CinemaCritic.API.Data;
using CinemaCritic.API.Models;
using CinemaCritic.API.Models.JoinTables;
using CinemaCritic.API.Repositories.Contracts;
using CinemaCritic.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CinemaCritic.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MovieRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }

        public async Task<bool> CreateMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            return await SaveChanges();
        }

        public async Task<bool> DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
            return await SaveChanges();
        }

        public async Task<ICollection<Movie>> GetAllMovies()
        {
            return await _context.Movies.Include(m => m.Genre).ToListAsync();
        }
        public async Task<ICollection<TopMoviesDto>> GetTopMovies()
        {
            var topMovies = await _context.Movies
                .Include(m => m.Reviews)
                .Select(m => new
                {
                    m.Id,
                    m.Name,
                    AverageRating = m.Reviews.Any() ? m.Reviews.Average(r => r.Rating) : 0.0,
                    m.ImageUrl
                })
                .OrderByDescending(m => m.AverageRating)
                .Take(20)
                .ToListAsync();
            ICollection<TopMoviesDto> movies= new List<TopMoviesDto>();
            foreach (var movie in topMovies)
            {
                movies.Add(new TopMoviesDto { Id = movie.Id, Rating = movie.AverageRating, Name = movie.Name, ImageUrl = movie.ImageUrl });
            }
            return movies;
        }

        public async Task<Movie> GetMovie(int id)
        {
            return await _context.Movies.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Movie> GetMovieDetails(int id)
        {
            return await _context.Movies.Where(m => m.Id == id).Include(m => m.Genre).FirstOrDefaultAsync();
        }

        public async Task<bool> MovieExists(int id)
        {
            return await _context.Movies.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            return await SaveChanges();
        }
    }
}

using CinemaCritic.API.Models;
using CinemaCritic.API.Models.JoinTables;
using Microsoft.EntityFrameworkCore;

namespace CinemaCritic.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserMovies> UserMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMovies>()
                .HasKey(um => new {um.UserId, um.MovieId});

            modelBuilder.Entity<UserMovies>()
                .HasOne(um => um.User)
                .WithMany(u => u.UserMovies)
                .HasForeignKey(um => um.UserId)
                .IsRequired(false);

            modelBuilder.Entity<UserMovies>()
                .HasOne(um => um.Movie)
                .WithMany(m => m.UserMovies)
                .HasForeignKey(um => um.MovieId)
                .IsRequired(false);
        }
    }
}

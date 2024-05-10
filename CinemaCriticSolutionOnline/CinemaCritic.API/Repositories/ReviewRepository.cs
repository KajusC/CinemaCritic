using CinemaCritic.API.Data;
using CinemaCritic.API.Models;
using CinemaCritic.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaCritic.API.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            return await SaveChanges();
        }

        public async Task<bool> DeleteReview(Review review)
        {
            _context.Reviews.Remove(review);
            return await SaveChanges();
        }

        public async Task<ICollection<Review>> GetAllReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetReview(int id)
        {
            return await _context.Reviews.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Review>> GetReviewsOfMovie(int movieId)
        {
            return await _context.Reviews.Include(r => r.User).Where(r => r.Movie.Id == movieId).ToListAsync();
        }

        public async Task<ICollection<Review>> GetReviewsOfUser(int userId)
        {
            return await _context.Reviews.Include(r => r.Movie).Where(r => r.User.Id == userId).ToListAsync();
        }

        public async Task<bool> ReviewExists(int id)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            try{
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            return await SaveChanges();
        }
    }
}

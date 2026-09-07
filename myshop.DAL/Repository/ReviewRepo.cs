using Microsoft.EntityFrameworkCore;
using myshop.DAL.IRepository;
using myshop.DAL.Models;
using myshop.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Repository
{
    public class ReviewRepo : GenericRepo<Review> , IReviewRepo
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> GetReviewUsingUserIdAndProductId(string userId, int productId)
        {
            var reviewWithUserId = await _context.Reviews.FirstOrDefaultAsync(r => r.userId == userId);
            var reviewWithProductId = await _context.Reviews.FirstOrDefaultAsync( r => r.productId == productId);
            if (reviewWithUserId != null || reviewWithProductId == null)
                return false;
            if (reviewWithUserId.reviewId == reviewWithProductId.reviewId)
                return true;
            return false;
        }
    }
}

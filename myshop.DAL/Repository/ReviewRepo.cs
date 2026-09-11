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
            return await _context.Reviews
        .AnyAsync(r => r.userId == userId && r.productId == productId);
        }

        
    }
}

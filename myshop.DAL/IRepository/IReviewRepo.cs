using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.IRepository
{
    public interface IReviewRepo : IGenericRepo<Review>
    {
        public Task<bool> GetReviewUsingUserIdAndProductId(string userId,int productId);
    }
}

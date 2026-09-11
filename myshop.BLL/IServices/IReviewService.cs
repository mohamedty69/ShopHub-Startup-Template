using myshop.BLL.DTOs.ReviewDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface IReviewService
    {
        public Task<bool> AddReviewAsync(CreateReviewDTO createReviewDto);
        public Task<bool> EditReviewAsync(EditReviewDTO editReviewDto);
        public Task<bool> DeleteReviewAsync(int reviewId);
        public Task<IEnumerable<DisplayReviewDTO>> GetAllReviewsAsync();
        public Task<bool> HasUserReviewedProductAsync(int productId, string userId);
        public Task<string> GetUserIdUsingReviewId(int reviewId);
        public Task<int> GetProductIdUsingReviewId(int reviewId);

    }
}

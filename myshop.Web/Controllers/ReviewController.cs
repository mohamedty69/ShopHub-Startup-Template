using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.ReviewDTO;
using myshop.BLL.IServices;
using System.Security.Claims;

namespace myshop.PL.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> AddReview(CreateReviewDTO createReviewDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            createReviewDTO.userId = userId; 
            if (ModelState.IsValid)
            {
                await _reviewService.AddReviewAsync(createReviewDTO);
            }
            else 
            {
                foreach (var error in ModelState.Keys)
                {
                    ModelState.AddModelError($"{error}", "Fix it");
                }
            }
            return RedirectToAction( "DisplayProductDetails", "Customer", new { id = createReviewDTO.productId});
        }
        public async Task<IActionResult> EditReview(EditReviewDTO editReviewDTO)
        {
            var activeUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reviewUserId = await _reviewService.GetUserIdUsingReviewId(editReviewDTO.reviewId);
            if (reviewUserId != activeUserId)
                throw new InvalidOperationException("You do not have access to do that, each user can edit it is own review only");
            await _reviewService.EditReviewAsync(editReviewDTO);
            var productId = await _reviewService.GetProductIdUsingReviewId(editReviewDTO.reviewId);
            return RedirectToAction("DisplayProductDetails", "Customer", new { id = productId });
        }
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var activeUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reviewUserId = await _reviewService.GetUserIdUsingReviewId(reviewId);
            if (reviewUserId != activeUserId)
                throw new InvalidOperationException("You do not have access to do that, each user can delete it is own review only");
            await _reviewService.DeleteReviewAsync(reviewId);
            var productId = await _reviewService.GetProductIdUsingReviewId(reviewId);
            return RedirectToAction("DisplayProductDetails", "Customer", new {id = productId});
        }

    }
}

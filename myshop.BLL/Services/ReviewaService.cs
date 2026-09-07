using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using myshop.BLL.DTOs.ReviewDTO;
using myshop.BLL.IServices;
using myshop.DAL.Iconfiguration;
using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Services
{
    public class ReviewaService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Review PrepareReview(CreateReviewDTO createReviewDto)
        {
            if (createReviewDto == null)
                throw new ArgumentNullException("You need to write a review first");
            var mappedReview = _mapper.Map<Review>(createReviewDto);
            return mappedReview;
        }
        public async Task<bool> AddReviewAsync(CreateReviewDTO createReviewDto)
        {
            var addCheck = await _unitOfWork.reviewRepo.Add(PrepareReview(createReviewDto));
            if (addCheck)
            {
                await _unitOfWork.CompleteTask();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var deleteCheck = await _unitOfWork.reviewRepo.Delete(reviewId);
            if (deleteCheck)
            {
                await _unitOfWork.CompleteTask();
                return true;
            }
            return false;
        }

        public async Task<bool> EditReviewAsync(EditReviewDTO editReviewDto)
        {
            if (string.IsNullOrWhiteSpace( editReviewDto.comment) || editReviewDto.rating == null)
                throw new ArgumentNullException("You need to fill the fields with values first");
            var existingReview = await _unitOfWork.reviewRepo.GetById(editReviewDto.reviewId);
            if (existingReview == null)
                throw new NullReferenceException("The review can not be found");
            if (existingReview.comment != editReviewDto.comment || existingReview.rating != editReviewDto.rating )
            {
                existingReview.comment = editReviewDto.comment;
                existingReview.rating = editReviewDto.rating?? throw new NullReferenceException("Rating field can not be null");
                await _unitOfWork.reviewRepo.Update(existingReview);
                await _unitOfWork.CompleteTask();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<DisplayReviewDTO>> GetAllReviewsAsync()
        {
            var listOfReviews = await _unitOfWork.reviewRepo.GetAll();
            var mappedListOfReviews = _mapper.Map<IEnumerable<DisplayReviewDTO>>(listOfReviews);
            return mappedListOfReviews;
        }
        public async Task<bool> HasUserReviewedProductAsync(int productId, string userId)
        {
            var check = await _unitOfWork.reviewRepo.GetReviewUsingUserIdAndProductId(userId, productId);
            return check;
        }
    }
}

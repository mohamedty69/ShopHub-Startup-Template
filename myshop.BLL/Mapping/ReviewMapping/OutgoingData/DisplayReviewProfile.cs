using AutoMapper;
using myshop.BLL.DTOs.ReviewDTO;
using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.ReviewMapping.OutgoingData
{
    public class DisplayReviewProfile : Profile
    {
        public DisplayReviewProfile()
        {
            CreateMap<Review, DisplayReviewDTO>();
        }
    }
}

using AutoMapper;
using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.ReviewMapping.OutgoingData
{
    public class DisplayReviewDTO : Profile
    {
        public DisplayReviewDTO()
        {
            CreateMap<Review, DisplayReviewDTO>();
        }
    }
}

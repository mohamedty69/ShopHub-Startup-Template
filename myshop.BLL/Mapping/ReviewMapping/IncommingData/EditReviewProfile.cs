using AutoMapper;
using myshop.BLL.DTOs.ReviewDTO;
using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.ReviewMapping.IncommingData
{
    public class EditReviewProfile : Profile
    {
        public EditReviewProfile()
        {
            CreateMap<EditReviewDTO, Review>();
        }
    }
}

using AutoMapper;
using myshop.BLL.DTOs.ReviewDTO;
using myshop.DAL.Models;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.ReviewMapping.IncommingData
{
    public class CreateReviewProfile : Profile
    {
        public CreateReviewProfile()
        {
            CreateMap<CreateReviewDTO, Review>();
        }
    }
}

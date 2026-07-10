using AutoMapper;
using myshop.BLL.DTOs.Category;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.CategoryMapping.IncomingDate
{
    public class CreateCategoryProfile : Profile
    {
        public CreateCategoryProfile()
        {
            CreateMap<CategoryDTO, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}

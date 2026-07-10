using AutoMapper;
using myshop.BLL.DTOs.Category;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Mapping.CategoryMapping.OutcomingData
{
    public class DisplayCategoryProfile: Profile
    {
        public DisplayCategoryProfile()
        {
            
            CreateMap<Category, CategoryDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedTime));
        }
    }
}

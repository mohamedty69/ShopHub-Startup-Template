using AutoMapper;
using myshop.BLL.DTOs.Category;
using myshop.BLL.IServices;
using myshop.DAL.Iconfiguration;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreaetCategoryAsync(CategoryDTO category)
        {
            var mappedCategory = _mapper.Map<CategoryDTO, Category>(category);
            var check = await _unitOfWork.Categories.Add(mappedCategory);
            if (check)
            {
                await _unitOfWork.CompleteTask();
                return true;
            }
            else throw new InvalidOperationException("The category can not created");
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _unitOfWork.Categories.Delete(id);
            await _unitOfWork.CompleteTask();
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync() 
        {
            var listOfCategory = await _unitOfWork.Categories.GetAll();
            var mappedList = _mapper.Map<IEnumerable<CategoryDTO>>(listOfCategory);
            return mappedList;
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.FindCategoryByIdAsync(id);
            var mappedCategory = _mapper.Map<CategoryDTO>(category);
            return mappedCategory;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var mappedCategory = _mapper.Map<Category>(categoryDTO);
            var check = await _unitOfWork.Categories.Update(mappedCategory);
            if (check)
            { 
                await _unitOfWork.CompleteTask();
                return true;
            }
            return false;
        }
    }
}

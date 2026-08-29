using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using myshop.BLL.DTOs.Category;
using myshop.BLL.DTOs.Product.Admin;
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
        private readonly IMemoryCache _memoryCache;
        private const string CategoryListCacheKey = "CategoryListCacheKey";

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }
        public async Task<bool> CreaetCategoryAsync(CategoryDTO category)
        {
            var mappedCategory = _mapper.Map<CategoryDTO, Category>(category);
            var check = await _unitOfWork.Categories.Add(mappedCategory);
            if (check)
            {
                await _unitOfWork.CompleteTask();
                _memoryCache.Remove(CategoryListCacheKey);
                return true;
            }
            else throw new InvalidOperationException("The category can not created");
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _unitOfWork.Categories.Delete(id);
            await _unitOfWork.CompleteTask();
            _memoryCache.Remove(CategoryListCacheKey);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync() 
        {
            if (!_memoryCache.TryGetValue(CategoryListCacheKey, out IEnumerable<CategoryDTO>? mappedList))
            {
                var listOfCategory = await _unitOfWork.Categories.GetAll();
                mappedList = _mapper.Map<IEnumerable<CategoryDTO>>(listOfCategory);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(CategoryListCacheKey, mappedList, cacheOptions);
            }
            return mappedList!;
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
                _memoryCache.Remove(CategoryListCacheKey);
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllDeletedCategories()
        {
            var listOfDeletedCategories = await _unitOfWork.Categories.GetDeletedCategories();
            var mappedList = _mapper.Map<IEnumerable<CategoryDTO>>(listOfDeletedCategories);
            return mappedList;
        }

        public async Task<bool> RestoreCategoryAsync(int categoryId)
        {
            try
            {
                var listOfdeletedCategories= await GetAllDeletedCategories();
                var deletedCategory = listOfdeletedCategories.FirstOrDefault(c => c.Id == categoryId);
                deletedCategory?.IsDeleted = false;
                var mappedCategory= _mapper.Map<Category>(deletedCategory);
                var check = await _unitOfWork.Categories.Update(mappedCategory);
                if (check)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

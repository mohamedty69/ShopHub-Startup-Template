using myshop.BLL.DTOs.Category;
using myshop.BLL.DTOs.Product.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface ICategoryService
    {
        public Task<bool> CreaetCategoryAsync(CategoryDTO category);
        public Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        public Task<CategoryDTO> GetCategoryByIdAsync(int id);
        public Task<bool> UpdateCategoryAsync(CategoryDTO categoryDTO);
        public Task DeleteCategoryAsync(int id);
        public Task<IEnumerable<CategoryDTO>> GetAllDeletedCategories();
        public Task<bool> RestoreCategoryAsync(int categoryId);
    }
}
